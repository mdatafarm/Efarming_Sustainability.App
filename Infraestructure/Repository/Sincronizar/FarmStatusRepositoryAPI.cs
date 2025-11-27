using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class FarmStatusRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public FarmStatusRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/FarmStatus/");
        }

        public async Task<List<FarmStatus>> GetFarmStatuses()
        {
            try
            {
                var result = await _api.GetAsync<List<FarmStatus>>("getFarmStatus");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estados de finca: {ex.Message}");
                return new List<FarmStatus>();
            }
        }
    }
}