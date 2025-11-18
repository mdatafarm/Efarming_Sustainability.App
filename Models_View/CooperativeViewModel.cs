using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class CooperativeViewModel
    {
        private readonly CooperativeRepository _cooperativeRepository;

        public CooperativeViewModel(CooperativeRepository cooperativeRepository)
        {
            _cooperativeRepository = new CooperativeRepository();
        }

        public List<Cooperative> Items { get; set; }

        public async Task LoadCooperativesAsync()
        {
            try
            {
                Items = await _cooperativeRepository.GetLocalCooperatives();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading cooperatives: {ex.Message}");
                Items = new List<Cooperative>();
            }
        }
    }
}