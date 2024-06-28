using Microsoft.EntityFrameworkCore;
using SportsMatchScoring.Api.Handlers;
using SportsMatchScoring.Api.Interfaces;
using SportsMatchScoring.Data;
using SportsMatchScoring.Repository;
using SportsMatchScoring.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);
 
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameHandler, GameHandler>();
builder.Services.AddScoped<IMatchRecordRepository, MatchRecordRepository>();
builder.Services.AddDbContext<SportsMatchContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))) ;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(["http://localhost:4200"])
                            .AllowAnyMethod()
                            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// For integration testing
public partial class Program { }