using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlaceGuesser.Data;
using PlaceGuesser.Interfaces;
using PlaceGuesser.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGamesRepository, GamesRepository>();
builder.Services.AddScoped<IVideosRepository, VideosRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<VideoDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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