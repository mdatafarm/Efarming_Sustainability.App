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
    public class ProductivityRepositoryAPI
    {
        private readonly string _baseUrl = "https://localhost:7292/api/Productivity/";

        public ProductivityRepositoryAPI()
        {
        }

        public async Task<List<Productivity>> GetProductivitybyId(Guid Id)
        {
            try
            {
                var api = new ConsumoApi(_baseUrl);
                string endpoint = $"getProductivity?FarmId={Id}";

                if (Id == Guid.Empty)
                {
                    throw new ArgumentException("El Id no puede ser vacío.", nameof(Id));
                }

                var result = await api.GetAsync<List<Productivity>>(endpoint);
                return result ?? new List<Productivity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la productividad desde API: {ex.Message}");
                return new List<Productivity>();
            }

        }

        public async Task<bool> UpdateProductivityAsync(Productivity productivity)
        {
            try
            {
                using var http = new HttpClient { BaseAddress = new Uri(_baseUrl) };
                var json = JsonSerializer.Serialize(productivity, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync("updateProductivity", content);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                    Console.WriteLine($"Error API UpdateProductivity: {response.StatusCode} - {body}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando la productividad en la API: {ex.Message}");
                return false;
            }
        }


    }
}
