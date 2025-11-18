using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class VillageRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public VillageRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/Village/");
        }

        public async Task<List<Village>> GetVillages()
        {
            try
            {
                var villagesResult = await _api.GetAsync<List<Village>>("getVillages");
                return villagesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las veredas/poblados: {ex.Message}");
                return new List<Village>();
            }
        }
    }
}