using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar;
using Efarming_Sustainability.Core.Models;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class FarmsRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public FarmsRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Farms>();
        }

        public async Task<List<Farms>> GetFarms(Guid userId, Guid? villageId = null, string? code = null)
        {
            var api = new ConsumoApi("https://localhost:7292/api/Farms/");
            try
            {
                
                string endpoint = $"GetFarms/{userId}";

                
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(code))
                {
                   
                    queryParams.Add($"Code={Uri.EscapeDataString(code)}");
                }

                if (villageId.HasValue)
                {
                    
                    queryParams.Add($"VillageId={villageId.Value}");
                }

                
                if (queryParams.Any())
                {
                    endpoint += $"?{string.Join("&", queryParams)}";
                }

                var result = await api.GetAsync<List<Farms>>(endpoint);
                return result ?? new List<Farms>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las fincas: {ex.Message}");
                return new List<Farms>();
            }
        }

        public async Task SaveFarmsLocally(List<FarmLocal> farms)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<FarmLocal>();
            await _db.InsertAllAsync(farms);
        }

        public async Task<List<Farms>> GetLocalFarms()
        {
            await InitializeAsync();
            return await _db.Table<Farms>().ToListAsync();
        }

        public async Task DeleteAllFarms()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Farms>();
        }

        
        public async Task<int> UpdateFarmLocalAsync(Farms farm)
        {
            try
            {
                await InitializeAsync();
                return await _db.UpdateAsync(farm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la finca localmente: {ex.Message}");
                return 0;
            }
        }

       
        public async Task<bool> UpdateFarmAndSyncAsync(Farms farm)
        {
            try
            {
                var rows = await UpdateFarmLocalAsync(farm);

                if (rows <= 0)
                {
                    return false;
                }

                var api = new FarmsRepositoryAPI();
                var apiResult = await api.UpdateFarmAsync(farm);

                if (!apiResult)
                {
                    Console.WriteLine("La sincronización con la API falló después de actualizar localmente.");
                }

                return apiResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando y sincronizando la finca: {ex.Message}");
                return false;
            }
        }

        
        public int UpdateFarm(Farms farm)
        {
            try
            {
                return UpdateFarmLocalAsync(farm).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateFarm síncrono: {ex.Message}");
                return 0;
            }
        }
    }
}
