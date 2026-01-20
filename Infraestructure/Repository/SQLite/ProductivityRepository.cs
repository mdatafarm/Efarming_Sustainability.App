using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class ProductivityRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public ProductivityRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Productivity>();
        }

        public async Task SaveProductivityLocal(List<Productivity> productivity)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Productivity>();
            await _db.InsertAllAsync(productivity);
        }

        public async Task SaveProductivityLocally(List<Productivity> productivity)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Productivity>();
            await _db.InsertAllAsync(productivity);
        }

        public async Task<List<Productivity>> GetAllProductivity()
        {
            await InitializeAsync();
            return await _db.Table<Productivity>().ToListAsync();
        }

        public async Task<List<Productivity>> GetProductivityByFarmId(Guid farmId)
        {
            try
            {
                return await _db.Table<Productivity>()
                    .Where(p => p.Id == farmId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo la productividad por FarmId: {ex.Message}");
                return new List<Productivity>();
            }
        }

        public async Task<int>UpdateProductivityLocalAsync(Productivity productivity)
        {
            try
            {
                await InitializeAsync();
                return await _db.UpdateAsync(productivity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la productividad localmente: {ex.Message}");
                return 0;
            }
        }



    }
}
