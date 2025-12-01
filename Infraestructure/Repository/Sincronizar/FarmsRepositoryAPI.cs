using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class FarmsRepositoryAPI
    {
        private readonly string _baseUrl = "https://localhost:7292/api/Farms/";

        public FarmsRepositoryAPI()
        {
        }

        public async Task<List<Farm>> GetFarms(Guid userId, Guid? villageId = null, string? code = null)
        {
            try
            {
                var api = new ConsumoApi(_baseUrl);
                string endpoint = $"getFarms?UserId={userId}";

                if (!string.IsNullOrEmpty(code))
                {
                    endpoint += $"&Code={Uri.EscapeDataString(code)}";
                }

                if (villageId.HasValue)
                {
                    endpoint += $"&VillageId={villageId}";
                }

                var farmsResult = await api.GetAsync<List<Farm>>(endpoint);
                return farmsResult ?? new List<Farm>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las fincas desde API: {ex.Message}");
                return new List<Farm>();
            }
        }

        public async Task<bool> UpdateFarmAsync(Farm farm)
        {
            try
            {
                using var http = new HttpClient { BaseAddress = new Uri(_baseUrl) };
                var json = JsonSerializer.Serialize(farm, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

               
                var response = await http.PostAsync("UpdateFarms", content);

                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error API UpdateFarm: {response.StatusCode} - {body}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la finca en la API: {ex.Message}");
                return false;
            }
        }
    }
}