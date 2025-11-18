using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class MunicipalityRepositoryAPI
    {
        private readonly ConsumoApi _api;
        public MunicipalityRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/Municipality/");
        }
        public async Task<List<Municipality>> GetMunicipalities()
        {
            try
            {
                var municipalitiesResult = await _api.GetAsync<List<Municipality>>("getMunicipalities");
                return municipalitiesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los municipios{ex.Message}");
                return new List<Municipality>();
            }
        }
    }
}
