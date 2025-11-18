using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class SupplyChainsRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public SupplyChainsRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/SupplyChains/");
        }

        public async Task<List<SupplyChains>> GetSupplyChains()
        {
            try
            {
                var result = await _api.GetAsync<List<SupplyChains>>("getSupplyChains");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las cadenas de suministro: {ex.Message}");
                return new List<SupplyChains>();
            }
        }
    }
}