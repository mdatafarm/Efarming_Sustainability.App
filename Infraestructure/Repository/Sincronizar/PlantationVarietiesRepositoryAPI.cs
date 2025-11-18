using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class PlantationVarietiesRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public PlantationVarietiesRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/PlantationVarieties/");
        }

        public async Task<List<PlantationVarieties>> GetPlantationVarieties()
        {
            try
            {
                var result = await _api.GetAsync<List<PlantationVarieties>>("getPlantationVarieties");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las variedades de plantación: {ex.Message}");
                return new List<PlantationVarieties>();
            }
        }
    }
}