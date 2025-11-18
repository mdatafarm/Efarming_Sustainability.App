using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class IndicatorsRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public IndicatorsRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Indicators>();
        }


        public async Task SaveIndicatorsLocally(List<Indicators> indicators)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Indicators>();
            foreach (var indicator in indicators)
            {

                await _db.InsertAsync(indicator);
            }
        }

        public async Task<Indicators> GetLocallyIndicators()
        {
            await InitializeAsync();
            var indicators = await _db.Table<Indicators>().FirstOrDefaultAsync();
            return indicators;
        }

    }
}