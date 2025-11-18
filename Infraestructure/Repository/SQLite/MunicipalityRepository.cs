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
    public class MunicipalityRepository : SQLiteConnection
    {
        private readonly HttpClient httpClient;

        public MunicipalityRepository()
        {
            httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Municipality>();
        }

        public async Task<List<Municipality>> GetMunicipalities()
        {
            var api = new ConsumoApi("https://localhost:7292/api/Municipalities/");
            try
            {
                var municipalitiesResult = await api.GetAsync<List<Municipality>>("getMunicipalities");
                return municipalitiesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los municipios{ex.Message}");
                return new List<Municipality>();
            }

        }



        public async Task SaveMunicipalityLocally(List<Municipality> municipalities)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Municipality>();
            await _db.InsertAllAsync(municipalities);

        }

        public async Task<List<Municipality>> GetLocalMunicipalities()
        {
            await InitializeAsync();
            return await _db.Table<Municipality>().ToListAsync();
        }
    }
}
