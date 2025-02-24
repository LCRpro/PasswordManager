using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PasswordManager.Web;
using System.Net.Http;
using PasswordManager.Web.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddBlazoredLocalStorage(); // Ajoute le service LocalStorage
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5233/api/") });
builder.Services.AddScoped<AuthService>(); // Service pour l'authentification
builder.Services.AddScoped<PasswordService>(); // Service pour les mots de passe
await builder.Build().RunAsync();
