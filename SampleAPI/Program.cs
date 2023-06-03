using Common.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Configuration;
using SampleAPI.Data;
using SampleAPI.Data.Repositories;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwagger();
builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.ConfigureRateLimiting();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.MapGet("/ping", (ISwaggerProvider swaggerProvider) =>
{
    var swaggerDoc = swaggerProvider.GetSwagger("v1");
    var title = swaggerDoc.Info.Description;
    var version = swaggerDoc.Info.Version;
    var message = $"{title} Version {version}";

    return Task.FromResult(Results.Ok(message));
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<AppDbContext>()!;
    dbContext.Database.Migrate();
}

app.Run();