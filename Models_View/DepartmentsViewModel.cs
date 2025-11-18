using Efarming_Sustainability.App.Infraestructure.Repository.SQLite;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Models_View
{
    class DepartmentsViewModel
    {
        private readonly DepartmentsRepository _departmentsRepository;

        public DepartmentsViewModel(DepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = new DepartmentsRepository();
        }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? Code { get; set; }
        public List<Department> Items { get; set; }

        public async Task LoadDepartmentsAsync()
        {
            try
            {
                Items = await _departmentsRepository.GetLocalDepartments();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading departments: {ex.Message}");
                Items = new List<Department>();
            }
        }
    }
}