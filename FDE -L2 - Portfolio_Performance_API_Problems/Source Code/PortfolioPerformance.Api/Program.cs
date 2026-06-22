using PortfolioPerformance.Api.Services;
using PortfolioPerformance.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PricingService>();
builder.Services.AddSingleton<IdempotencyService>();

builder.Services.AddScoped<IAttributionService, AttributionService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();