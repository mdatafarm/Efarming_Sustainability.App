using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class PlantationTypesRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public PlantationTypesRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/PlantationTypes/");
        }

        public async Task<List<PlantationTypes>> GetPlantationTypes()
        {
            try
            {
                var result = await _api.GetAsync<List<PlantationTypes>>("getPlantationTypes");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los tipos de plantación: {ex.Message}");
                return new List<PlantationTypes>();
            }
        }
    }
}