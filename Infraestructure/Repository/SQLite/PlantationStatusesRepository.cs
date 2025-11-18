using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class PlantationStatusesRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public PlantationStatusesRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<PlantationStatuses>();
        }

        public async Task<List<PlantationStatuses>> GetPlantationStatuses()
        {
            var api = new ConsumoApi("https://localhost:7292/api/PlantationStatuses/");
            try
            {
                var result = await api.GetAsync<List<PlantationStatuses>>("getPlantationStatuses");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estados de plantación: {ex.Message}");
                return new List<PlantationStatuses>();
            }
        }

        public async Task SavePlantationStatusesLocally(List<PlantationStatuses> statuses)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationStatuses>();
            await _db.InsertAllAsync(statuses);
        }

        public async Task<List<PlantationStatuses>> GetLocalPlantationStatuses()
        {
            await InitializeAsync();
            return await _db.Table<PlantationStatuses>().ToListAsync();
        }

        public async Task DeleteAllPlantationStatuses()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationStatuses>();
        }
    }
}