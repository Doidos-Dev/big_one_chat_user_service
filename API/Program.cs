using API;
using DependencyInjection.Ext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environment = APIConfiguration.LocalEnvironment;

builder.Services.AddConfig(environment == APIConfiguration.LocalEnvironment
    ? builder.Configuration["Postgres:ConnectionStrings"]
    : Environment.GetEnvironmentVariable("DATABASE_CONNECTION_SERVICE_USER"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
