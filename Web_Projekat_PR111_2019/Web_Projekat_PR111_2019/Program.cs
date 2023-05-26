using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Mapper;
using Web_Projekat_PR111_2019.Repositories;
using Web_Projekat_PR111_2019.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IKorisnikServis, KorisnikServis>();

builder.Services.AddScoped<IEmailServis, EmailServis>();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionWithDBWebProjekat"));
});

// Add AutoMapper configuration
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapDTO());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
