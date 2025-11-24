using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class FamilyUnitMembersRepositoryAPI
    {
        private readonly string _baseUrl = "https://localhost:7292/api/FamilyUnitMembers/";

        public FamilyUnitMembersRepositoryAPI()
        {
        }

        public async Task<List<FamilyUnitMembers>> GetFUM(Guid farmId)
        {
            try
            {
                var api = new ConsumoApi(_baseUrl);
                string endpoint = $"getFamilyUnitMembersByFarmId/{farmId}";

                if (farmId == Guid.Empty)
                {
                    throw new ArgumentException("El ID de la finca no puede estar vacío.");
                }

                var result = await api.GetAsync<List<FamilyUnitMembers>>(endpoint);
                return result ?? new List<FamilyUnitMembers>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las personas desde el API: {ex.Message}");
                return new List<FamilyUnitMembers>();
            }
        }


        public async Task<bool> UpdateFUMAsync(FamilyUnitMembers fum)
        {
            try
            {
                using var http = new HttpClient { BaseAddress = new Uri(_baseUrl) };
                var json = JsonSerializer.Serialize(fum, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await http.PutAsync("updateFamilyUnitMember", content);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error API UpdateFUM: {response.StatusCode} - {body}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la persona en la API: {ex.Message}");
                return false;
            }
        }
    }
}
