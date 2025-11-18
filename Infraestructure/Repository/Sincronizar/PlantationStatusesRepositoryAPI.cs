using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class PlantationStatusesRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public PlantationStatusesRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/PlantationStatuses/");
        }

        public async Task<List<PlantationStatuses>> GetPlantationStatuses()
        {
            try
            {
                var result = await _api.GetAsync<List<PlantationStatuses>>("getPlantationStatuses");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los estados de plantación: {ex.Message}");
                return new List<PlantationStatuses>();
            }
        }
    }
}