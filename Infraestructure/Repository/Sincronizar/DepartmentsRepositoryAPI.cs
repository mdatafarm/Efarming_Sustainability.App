using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class DepartmentsRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public DepartmentsRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/Departments/");
        }
        public async Task<List<Department>>GetDepartments()
        {
            try
            {
                var departmentsResult = await _api.GetAsync<List<Department>>("getDepartments");
                return departmentsResult;


            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al obtener los departamentos{ex.Message}");
                return new List<Department>();
            }
        }
    }
}
