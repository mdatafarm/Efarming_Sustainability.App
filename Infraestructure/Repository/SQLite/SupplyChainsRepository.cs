using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class SupplyChainsRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public SupplyChainsRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<SupplyChains>();
        }

        public async Task<List<SupplyChains>> GetSupplyChains()
        {
            var api = new ConsumoApi("https://localhost:7292/api/SupplyChains/");
            try
            {
                var result = await api.GetAsync<List<SupplyChains>>("getSupplyChains");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las cadenas de suministro: {ex.Message}");
                return new List<SupplyChains>();
            }
        }

        public async Task SaveSupplyChainsLocally(List<SupplyChains> supplyChains)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<SupplyChains>();
            await _db.InsertAllAsync(supplyChains);
        }

        public async Task<List<SupplyChains>> GetLocalSupplyChains()
        {
            await InitializeAsync();
            return await _db.Table<SupplyChains>().ToListAsync();
        }

        public async Task DeleteAllSupplyChains()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<SupplyChains>();
        }

        public async Task<SupplyChains?> GetSupplyChainsById(Guid? id)
        {
            await InitializeAsync();
            return await _db.Table<SupplyChains>()
                .Where(sc => sc.Id == id)
                .FirstOrDefaultAsync();
        }   
    }
}