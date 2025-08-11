using BookRadar.Data;
using BookRadar.Repositories;
using BookRadar.Repositories.Interfaces;
using BookRadar.Services;
using BookRadar.Services.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =========================================
// 1. Cargar variables de entorno (.env)
// =========================================
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("BOOKRADAR_DB");

// =========================================
// 2. Configuración de servicios
// =========================================

// Base de datos (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// MVC con vistas
builder.Services.AddControllersWithViews();

// Caché en memoria (para evitar búsquedas repetidas < 1 min)
builder.Services.AddMemoryCache();

// Cliente HTTP para API de Open Library
builder.Services.AddHttpClient("OpenLibrary", client =>
{
    client.BaseAddress = new Uri("https://openlibrary.org/");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("User-Agent", "BookRadar/1.0");
});

// Inyección de dependencias
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IHistorialRepository, HistorialRepository>();

var app = builder.Build();

// =========================================
// 3. Configuración del pipeline HTTP
// =========================================
if (!app.Environment.IsDevelopment())
{
    app.UseHsts(); // Seguridad HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}"
);

app.Run();
