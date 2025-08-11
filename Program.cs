using BookRadar.Data;
using BookRadar.Repositories;
using BookRadar.Services;
using BookRadar.Services.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Read var BOOKRADAR_DB
var connectionString = Environment.GetEnvironmentVariable("BOOKRADAR_DB");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();

// Registro de la API OpenLibrary
builder.Services.AddHttpClient("OpenLibrary", client =>
{
    client.BaseAddress = new Uri("https://openlibrary.org/");
    client.Timeout = TimeSpan.FromSeconds(10);
    client.DefaultRequestHeaders.Add("User-Agent", "BookRadar/1.0");
});

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IHistorialRepository, HistorialRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Search}");

app.Run();
