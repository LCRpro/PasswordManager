using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordManager.Web.Services
{
    public class PasswordService
    {
        private readonly HttpClient _http;

        public PasswordService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PasswordEntry>> GetPasswords(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await _http.GetFromJsonAsync<List<PasswordEntry>>("passwords") ?? new List<PasswordEntry>();
        }
    }

    public class PasswordEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string EncryptedPassword { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
