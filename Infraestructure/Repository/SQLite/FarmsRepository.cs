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
            await _db.CreateTableAsync<Farm>();
        }

        public async Task<List<Farm>> GetFarms(Guid userId, Guid? villageId = null, string? code = null)
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

                var result = await api.GetAsync<List<Farm>>(endpoint);
                return result ?? new List<Farm>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las fincas: {ex.Message}");
                return new List<Farm>();
            }
        }

        public async Task SaveFarmsLocally(List<Farm> farms)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Farm>();
            await _db.InsertAllAsync(farms);
        }

        public async Task<List<Farm>> GetLocalFarms()
        {
            await InitializeAsync();
            return await _db.Table<Farm>().ToListAsync();
        }

        public async Task DeleteAllFarms()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Farm>();
        }

        
        public async Task<int> UpdateFarmLocalAsync(Farm farm)
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

       
        public async Task<bool> UpdateFarmAndSyncAsync(Farm farm)
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

        
        public int UpdateFarm(Farm farm)
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
