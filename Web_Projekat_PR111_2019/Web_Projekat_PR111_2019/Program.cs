using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Web_Projekat_PR111_2019.Repository;
using Web_Projekat_PR111_2019.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionWithDBWebProjekat"));
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>(); 
});
;





// Registrujte AutoMapper konfiguraciju u servisnoj kolekciji
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddScoped<IRegistracijaRepository, RegistracijaRepository>();

builder.Services.AddScoped<IRegistracijaService, RegistracijaService>();



//byte[] ConvertIFormFileToByteArray(IFormFile file, ResolutionContext context)
//{
//    using (var memoryStream = new MemoryStream())
//    {
//        file.CopyTo(memoryStream);
//        return memoryStream.ToArray();
//    }
//}



//IFormFile ConvertByteArrayToIFormFile(byte[] byteArray, ResolutionContext context)
//{
//    var memoryStream = new MemoryStream(byteArray);
//    var formFile = new FormFile(memoryStream, 0, byteArray.Length, null, "file");

//    return formFile;
//}



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


public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<DTOFormaRegistracije, Korisnik>();
        CreateMap<Korisnik, DTOKorisnik>();

        CreateMap<IFormFile, byte[]>().ConvertUsing((file, _, context) => ConvertIFormFileToByteArray(file, context));
        CreateMap<byte[], IFormFile>().ConvertUsing((byteArray, _, context) => ConvertByteArrayToIFormFile(byteArray, context));
    }


    public byte[] ConvertIFormFileToByteArray(IFormFile file, ResolutionContext context)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }



    public IFormFile ConvertByteArrayToIFormFile(byte[] byteArray, ResolutionContext context)
    {
        var memoryStream = new MemoryStream(byteArray);
        var formFile = new FormFile(memoryStream, 0, byteArray.Length, null, "file");

        return formFile;
    }


}