using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class PlantationStatusesViewModel
    {
        private readonly PlantationStatusesRepository _plantationStatusesRepository;

        public PlantationStatusesViewModel(PlantationStatusesRepository plantationStatusesRepository)
        {
            _plantationStatusesRepository = new PlantationStatusesRepository();
        }

        public List<PlantationStatuses> Items { get; set; }

        public async Task LoadPlantationStatusesAsync()
        {
            try
            {
                Items = await _plantationStatusesRepository.GetLocalPlantationStatuses();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading plantation statuses: {ex.Message}");
                Items = new List<PlantationStatuses>();
            }
        }
    }
}