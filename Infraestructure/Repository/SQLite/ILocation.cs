using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public interface ILocation
    {
        Task<Village> GetVillageById(Guid id);
        Task<Municipality> GetMunicipalityById(Guid id);
        Task<Department> GetDepartmentById(Guid id);

        Task<List<Department>> GetDepartments();
        Task<List<Municipality>> GetMunicipalitiesByDepartment(Guid deptId);
        Task<List<Village>> GetVillagesByMunicipality(Guid muniId);
    }
}
