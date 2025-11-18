using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class SupplyChainsViewModel
    {
        private readonly SupplyChainsRepository _supplyChainsRepository;

        public SupplyChainsViewModel(SupplyChainsRepository supplyChainsRepository)
        {
            _supplyChainsRepository = new SupplyChainsRepository();
        }

        public List<SupplyChains> Items { get; set; }

        public async Task LoadSupplyChainsAsync()
        {
            try
            {
                Items = await _supplyChainsRepository.GetLocalSupplyChains();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading supply chains: {ex.Message}");
                Items = new List<SupplyChains>();
            }
        }
    }
}