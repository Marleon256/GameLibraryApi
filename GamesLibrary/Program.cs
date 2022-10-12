using GamesLibrary.DataAccess.Enities;
using GamesLibrary.Infrastructure.InfrustructureModels;
using GamesLibrary.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => 
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GamesContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddScoped<IRepository<Game>, GamesRepository>();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
