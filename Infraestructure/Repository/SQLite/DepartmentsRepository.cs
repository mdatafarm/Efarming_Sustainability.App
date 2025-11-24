using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class DepartmentsRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;
        public DepartmentsRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            await InitAsync();
            await _db.CreateTableAsync<Department>();
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var api = new ConsumoApi("https://localhost:7292/api/Departments/");

            try
            {
                var departmentsResult = await api.GetAsync<List<Department>>("getDepartments");
                return departmentsResult;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los departamentos{ex.Message}");
                return new List<Department>();
            }


        }

        public async Task SaveDepartamentsLocally(List<Department> departments)
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Department>();
            await _db.InsertAllAsync(departments);
            await GetLocalDepartments();

        }

        public async Task<List<Department>> GetLocalDepartments()
        {
            await InitializeAsync();
            return await _db.Table<Department>().ToListAsync();

        }

        public async Task DeleteAllDepartments()
        {
            await InitializeAsync();
            await _db.DeleteAllAsync<Department>();
        }

        public async Task<Department?> GetDepartmentById(Guid departmentId)
        {
            await InitializeAsync();
            return await _db.Table<Department>()
                            .Where(d => d.Id == departmentId)
                            .FirstOrDefaultAsync();
        }



    }
}
