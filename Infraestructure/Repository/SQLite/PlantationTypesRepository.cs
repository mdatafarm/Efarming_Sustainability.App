using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class PlantationTypesRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public PlantationTypesRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<PlantationTypes>();
        }

        public async Task<List<PlantationTypes>> GetPlantationTypes()
        {
            var api = new ConsumoApi("https://localhost:7292/api/PlantationTypes/");
            try
            {
                var result = await api.GetAsync<List<PlantationTypes>>("getPlantationTypes");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de plantación: {ex.Message}");
                return new List<PlantationTypes>();
            }
        }

        public async Task SavePlantationTypesLocally(List<PlantationTypes> types)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationTypes>();
            await _db.InsertAllAsync(types);
        }

        public async Task<List<PlantationTypes>> GetLocalPlantationTypes()
        {
            await InitializeAsync();
            return await _db.Table<PlantationTypes>().ToListAsync();
        }

        public async Task DeleteAllPlantationTypes()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<PlantationTypes>();
        }
    }
}