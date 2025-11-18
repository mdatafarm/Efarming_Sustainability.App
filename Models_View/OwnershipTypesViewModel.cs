using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class OwnershipTypesViewModel
    {
        private readonly OwnershipTypesRepository _ownershipTypesRepository;

        public OwnershipTypesViewModel(OwnershipTypesRepository ownershipTypesRepository)
        {
            _ownershipTypesRepository = new OwnershipTypesRepository();
        }

        public List<OwnershipTypes> Items { get; set; }

        public async Task LoadOwnershipTypesAsync()
        {
            try
            {
                Items = await _ownershipTypesRepository.GetLocalOwnershipTypes();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading ownership types: {ex.Message}");
                Items = new List<OwnershipTypes>();
            }
        }
    }
}