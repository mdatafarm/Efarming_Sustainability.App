using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    public class MunicipalityViewModel
    {
        private readonly MunicipalityRepository _municipalityRepository;

        public MunicipalityViewModel(MunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = new MunicipalityRepository();
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Guid DepartmentId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int Code { get; set; }


        public List<Municipality> Items { get; set; }

        public async Task LoadMunicipalitiesAsync()
        {
            try
            {
                
                Items = await _municipalityRepository.GetLocalMunicipalities();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading municipalities: {ex.Message}");
                Items = new List<Municipality>();
            }
        }
    }
}   