using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class OwnershipTypesRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public OwnershipTypesRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/OwnershipTypes/");
        }

        public async Task<List<OwnershipTypes>> GetOwnershipTypes()
        {
            try
            {
                var result = await _api.GetAsync<List<OwnershipTypes>>("getOwnershipTypes");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de propiedad: {ex.Message}");
                return new List<OwnershipTypes>();
            }
        }
    }
}