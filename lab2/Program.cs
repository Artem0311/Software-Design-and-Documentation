using lab2.Storage;
using lab2.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<JsonStorage>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapMessageEndpoints();

app.Run();
public partial class Program { }