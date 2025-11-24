using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class FamilyUnitMembersRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public FamilyUnitMembersRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<FamilyUnitMembers>();
        }

        public async Task<List<FamilyUnitMembers>> GetFUM(Guid farmId)
        {
            var api = new ConsumoApi("https://localhost:7292/api/FamilyUnitMembers/");

            try
            {
                string endepoint = $"getFamilyUnitMembers/{farmId}";
                var queryParams = new List<string>();

                if (!string.IsNullOrEmpty(farmId.ToString()))
                {
                    queryParams.Add($"farmId={farmId}");
                }

                var result = await api.GetAsync<List<FamilyUnitMembers>>(endepoint);
                return result ?? new List<FamilyUnitMembers>();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las personas de la unidad familiar: {ex.Message}");
                return new List<FamilyUnitMembers>();
            }
        }

        public async Task SaveFUMLocally(List<FamilyUnitMembers> fum)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<FamilyUnitMembers>();
            await _db.InsertAllAsync(fum);
        }


        public async Task<List<FamilyUnitMembers>> GetFUMLocalByFarmId(Guid farmId)
        {
            try
            {
                return await _db.Table<FamilyUnitMembers>()
                                .Where(fum => fum.FarmId == farmId)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las personas de la unidad familiar desde SQLite: {ex.Message}");
                return new List<FamilyUnitMembers>();
            }
        }

        public async Task<int> UpdateFUMLocalAsync(FamilyUnitMembers fum)
        {
            try
            {
                await InitializeAsync();
                var rows = await _db.UpdateAsync(fum);
                return rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la persona de la unidad familiar en SQLite: {ex.Message}");
                return 0;
            }
        }
    }
}
