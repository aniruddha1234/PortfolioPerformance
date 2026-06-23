using TransactionProcessor.BackgroundServices;
using TransactionProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TransactionStore>();
builder.Services.AddSingleton<TransactionService>();

builder.Services.AddHostedService<TransactionProcessorService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();