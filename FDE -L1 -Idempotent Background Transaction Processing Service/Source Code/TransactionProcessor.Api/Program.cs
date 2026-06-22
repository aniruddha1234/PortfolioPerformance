using TransactionProcessor.Api.Background;
using TransactionProcessor.Api.Services;
using TransactionProcessor.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBackgroundQueue, BackgroundQueue>();
builder.Services.AddSingleton<TransactionProcessorService>();
builder.Services.AddSingleton<TransactionValidator>();

builder.Services.AddHostedService<TransactionWorker>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();