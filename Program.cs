using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
        // ✅ 加上 Scalar UI
    app.MapScalarApiReference(options =>
    {
        options.Title = "HotelListing API 文件";
        // 告訴 Scalar UI 去抓哪個文件
        options.OpenApiRoutePattern = "/openapi/{documentName}.json";
        // 可選：設定主題 (Moon = 暗色, Light = 亮色)
        // options.Theme = ScalarTheme.Moon;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
