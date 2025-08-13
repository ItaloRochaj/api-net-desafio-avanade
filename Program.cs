using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Entity Framework com SQLite (simplificada)
builder.Services.AddDbContext<OrganizadorContext>(options => 
    options.UseSqlite("Data Source=TarefasDB.db"));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo 
    { 
        Title = "Board de Tarefas API",  
        Version = "v1"
    });
});

var app = builder.Build();

// Habilitar arquivos estáticos (imagens, CSS, JS, etc.)
app.UseStaticFiles();

// Configuração para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tarefas API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();
