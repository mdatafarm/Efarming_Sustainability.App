using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class FarmStatusViewModel
    {
        private readonly FarmStatusRepository _farmStatusRepository;

        public FarmStatusViewModel(FarmStatusRepository farmStatusRepository)
        {
            _farmStatusRepository = new FarmStatusRepository();
        }

        public List<FarmStatus> Items { get; set; }

        public async Task LoadFarmStatusesAsync()
        {
            try
            {
                Items = await _farmStatusRepository.GetLocalFarmStatuses();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading farm statuses: {ex.Message}");
                Items = new List<FarmStatus>();
            }
        }
    }
}