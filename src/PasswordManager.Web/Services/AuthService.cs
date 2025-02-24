using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PasswordManager.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string?> Login(string username, string password)
        {
            var response = await _http.PostAsJsonAsync("auth/login", new { Username = username, PasswordHash = password });

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return result?.Token;
        }
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
