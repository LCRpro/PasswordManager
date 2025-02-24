using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PasswordManager.Core;

var builder = WebApplication.CreateBuilder(args);

// Configuration du contexte SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=passwordmanager.db"));

// Ajout des services API et Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Password Manager API", Version = "v1" });
});

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Password Manager API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
