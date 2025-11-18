using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class IndicatorsRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public IndicatorsRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/Indicators/");
        }

        public async Task<List<Indicators>> GetIndicatorsAsync(Guid UserId)
        {
            try
            {
               
                var indicatorsResult = await _api.GetAsync<List<Indicators>>($"getIndicators?UserId={UserId}");

                if (indicatorsResult == null)
                {
                    throw new Exception("Fetching indicators failed: No response from server.");
                }

                return indicatorsResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching indicators: {ex.Message}");
                return null;
            }
        }
    }
}
