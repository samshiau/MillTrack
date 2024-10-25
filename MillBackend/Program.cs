using Microsoft.EntityFrameworkCore;
using MillBackend.Services;
using DotNetEnv;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);


Env.Load();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
string? constring = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
Console.WriteLine($"\n\n\nConnection String: {constring} \n\n\n");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(constring);
});

var app = builder.Build();

//Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();

Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("DefaultConnection")}");



