using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class VillageRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public VillageRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Village>();
        }

        public async Task<List<Village>> GetVillages()
        {
            var api = new ConsumoApi("https://localhost:7292/api/Villages/");
            try
            {
                var villagesResult = await api.GetAsync<List<Village>>("getVillages");
                return villagesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las veredas/poblados: {ex.Message}");
                return new List<Village>();
            }
        }

        public async Task SaveVillagesLocally(List<Village> villages)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Village>();
            await _db.InsertAllAsync(villages);
        }

        public async Task<List<Village>> GetLocalVillages()
        {
            await InitializeAsync();
            return await _db.Table<Village>().ToListAsync();
        }

        public async Task<Village> GetVillagesById(Guid Id)
        {
            await InitializeAsync();
            return await _db.Table<Village>()
                            .Where(v => v.Id == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<Village>> GetVillagesByMunicipality(Guid municipalityId)
        {
            await InitializeAsync();
            return await _db.Table<Village>()
                            .Where(v => v.MunicipalityId == municipalityId)
                            .ToListAsync();
        }
    }
}