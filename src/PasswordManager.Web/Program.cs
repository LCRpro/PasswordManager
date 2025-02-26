using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PasswordManager.Web;
using System.Net.Http;
using PasswordManager.Web.Services;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.Services.AddBlazoredLocalStorage(); 
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5233/api/") });
builder.Services.AddScoped<AuthService>(); 
builder.Services.AddScoped<PasswordService>(); 
await builder.Build().RunAsync();
