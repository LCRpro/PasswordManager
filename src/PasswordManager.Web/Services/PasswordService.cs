using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;

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
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _http.GetFromJsonAsync<List<PasswordEntry>>("passwords") ?? new List<PasswordEntry>();
        }
public async Task<bool> UpdatePassword(int id, PasswordEntry password, string token)
{
    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    Console.WriteLine($"🔄 Envoi de la requête PUT pour modifier le mot de passe ID {id}");

    var response = await _http.PutAsJsonAsync($"passwords/{id}", password);

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"❌ Erreur lors de la modification : {response.StatusCode}");
    }

    return response.IsSuccessStatusCode;
}

public async Task<bool> AddPassword(PasswordEntry password, string token)
{
    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    var jsonContent = System.Text.Json.JsonSerializer.Serialize(password);

    var response = await _http.PostAsJsonAsync("passwords", password);

    if (!response.IsSuccessStatusCode)
    {
        var errorMsg = await response.Content.ReadAsStringAsync();
    }

    return response.IsSuccessStatusCode;
}



        public async Task<bool> DeletePassword(int id, string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.DeleteAsync($"passwords/{id}");
            return response.IsSuccessStatusCode;
        }
    }

public class PasswordEntry
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string EncryptedPassword { get; set; } = string.Empty; 
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}


}
