using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class PlantationVarietiesViewModel
    {
        private readonly PlantationVarietiesRepository _plantationVarietiesRepository;

        public PlantationVarietiesViewModel(PlantationVarietiesRepository plantationVarietiesRepository)
        {
            _plantationVarietiesRepository = new PlantationVarietiesRepository();
        }

        public List<PlantationVarieties> Items { get; set; }

        public async Task LoadPlantationVarietiesAsync()
        {
            try
            {
                Items = await _plantationVarietiesRepository.GetLocalPlantationVarieties();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading plantation varieties: {ex.Message}");
                Items = new List<PlantationVarieties>();
            }
        }
    }
}