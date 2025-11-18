using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.Sincronizar
{
    public class CooperativeRepositoryAPI
    {
        private readonly ConsumoApi _api;

        public CooperativeRepositoryAPI()
        {
            _api = new ConsumoApi("https://localhost:7292/api/Cooperative/");
        }

        public async Task<List<Cooperative>> GetCooperatives()
        {
            try
            {
                var cooperativesResult = await _api.GetAsync<List<Cooperative>>("getCooperatives");
                return cooperativesResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las cooperativas: {ex.Message}");
                return new List<Cooperative>();
            }
        }
    }
}