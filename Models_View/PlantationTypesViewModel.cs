using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class PlantationTypesViewModel
    {
        private readonly PlantationTypesRepository _plantationTypesRepository;

        public PlantationTypesViewModel(PlantationTypesRepository plantationTypesRepository)
        {
            _plantationTypesRepository = new PlantationTypesRepository();
        }

        public List<PlantationTypes> Items { get; set; }

        public async Task LoadPlantationTypesAsync()
        {
            try
            {
                Items = await _plantationTypesRepository.GetLocalPlantationTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading plantation types: {ex.Message}");
                Items = new List<PlantationTypes>();
            }
        }
    }
}