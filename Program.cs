using Microsoft.OpenApi.Models;
using PdfMicroService.Data;
using PdfMicroService.Helpers;
using PdfMicroService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<PdfRepository>(sp=>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    string pathSave = configuration["PathSave"];
    return new PdfRepository(pathSave);
});

builder.Services.AddSingleton<PdfHelper>();
builder.Services.AddScoped<PdfService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PdfMicroService", Version = "v1" });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
