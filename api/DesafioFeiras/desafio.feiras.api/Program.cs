using desafio.feiras.api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddDesafioLogger(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<NotRequiredParameters>();
    options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "desafio.feiras.api.xml"));
    options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "desafio.feiras.application.xml"));
    options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "desafio.feiras.domain.xml"));
});
builder.Services.AddApplication();
builder.Services.AddInfrastructureMongoDB(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorMiddleware>();

app.MapControllers();

app.Run();
