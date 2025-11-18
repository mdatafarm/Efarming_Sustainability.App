using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class PlantationVarietiesRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public PlantationVarietiesRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<PlantationVarieties>();
        }

        public async Task<List<PlantationVarieties>> GetPlantationVarieties()
        {
            var api = new ConsumoApi("https://localhost:7292/api/PlantationVarieties/");
            try
            {
                var result = await api.GetAsync<List<PlantationVarieties>>("getPlantationVarieties");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las variedades de plantación: {ex.Message}");
                return new List<PlantationVarieties>();
            }
        }

        public async Task SavePlantationVarietiesLocally(List<PlantationVarieties> varieties)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationVarieties>();
            await _db.InsertAllAsync(varieties);
        }

        public async Task<List<PlantationVarieties>> GetLocalPlantationVarieties()
        {
            await InitializeAsync();
            return await _db.Table<PlantationVarieties>().ToListAsync();
        }

        public async Task DeleteAllPlantationVarieties()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationVarieties>();
        }
    }
}