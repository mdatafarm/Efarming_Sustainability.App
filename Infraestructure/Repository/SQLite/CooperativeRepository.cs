using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class CooperativeRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public CooperativeRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Cooperative>();
        }

        public async Task<List<Cooperative>> GetCooperatives()
        {
            var api = new ConsumoApi("https://localhost:7292/api/Cooperatives/");
            try
            {
                var cooperativesResult = await api.GetAsync<List<Cooperative>>("getCooperatives");
                return cooperativesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las cooperativas: {ex.Message}");
                return new List<Cooperative>();
            }
        }

        public async Task SaveCooperativesLocally(List<Cooperative> cooperatives)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Cooperative>();
            await _db.InsertAllAsync(cooperatives);
        }

        public async Task<List<Cooperative>> GetLocalCooperatives()
        {
            await InitializeAsync();
            return await _db.Table<Cooperative>().ToListAsync();
        }

        public async Task DeleteAllCooperatives()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Cooperative>();
        }
    }
}