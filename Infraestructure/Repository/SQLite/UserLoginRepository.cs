using Efarming_Sustainability.App.Infraestructure.API;
using Efarming_Sustainability.App.Infraestructure.Database;
using Efarming_Sustainability.Core.Models;
using Efarming_Sustainability.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efarming_Sustainability.App.Infraestructure.Repository.SQLite
{
    public class UserLoginRepository : SQLiteConnection
    {
        private readonly HttpClient _httpClient;

        public UserLoginRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> LoginUsuario(string username, string password)
        {
            var api = new ConsumoApi("https://localhost:7292/api/User/login");
            var loginData = new UserLogin
            {
                Username = username,
                Password = password
            };

            try
            {
                var loginResult = await api.PostAsync<UserLogin, LoginResponse>("login", loginData);

                if (loginResult == null)
                {
                    throw new Exception("Login failed: No response from server.");
                }

                return loginResult;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return null;
            }
        }
    }
}
