using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class VillageViewModel
    {
        private readonly VillageRepository _villageRepository;

        public VillageViewModel(VillageRepository villageRepository)
        {
            _villageRepository = new VillageRepository();
        }


        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Guid MunicipalityId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int Code { get; set; }

        public List<Village> Items { get; set; }

        public async Task LoadVillagesAsync()
        {
            try
            {
                // Si dispones de un método local cambialo por GetLocalVillages
                Items = await _villageRepository.GetLocalVillages();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading villages: {ex.Message}");
                Items = new List<Village>();
            }
        }
    }
}