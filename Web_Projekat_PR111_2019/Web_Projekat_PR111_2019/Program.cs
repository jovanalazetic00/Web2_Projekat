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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Dodajte servise u kontejner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web_Projekat_PR111_2019", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<Email>();


builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddScoped<IRegistracijaRepository, RegistracijaRepository>();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IArtikalRepository, ArtikalRepository>();
builder.Services.AddScoped<IPorudzbinaRepository, PorudzbinaRepository>();
builder.Services.AddScoped<IArtikalIPorudzbinaRepository, ArtikalIPorudzbinaRepository>();


builder.Services.AddScoped<IRegistracijaService, RegistracijaService>();
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IArtikalService, ArtikalService>();
builder.Services.AddScoped<IPorudzbinaService, PorudzbinaService>();
builder.Services.AddScoped<IEmailService, EmailService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "https://localhost:3000", "https://localhost:3001", "https://localhost:3002")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionWithDBWebProjekat"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
});

builder.Services.AddHttpContextAccessor();

// Dodajte konfiguraciju za autentifikaciju
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("jwtConfig");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("VerifikovanProdavac", policy => policy.RequireClaim("StatusVerifrikacije", "Verifikovan"));

});

var app = builder.Build();

// ...

// Konfigurišite HTTP zahtevni tok.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactAppPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<DTOFormaRegistracije, Korisnik>();
        CreateMap<Korisnik, DTOKorisnik>();
        CreateMap<Artikal, DTOArtikal>();
        CreateMap<DTODodajArtikal, Artikal>();
        CreateMap<Porudzbina, DTOPorudzbina>();
        CreateMap<DTODodajPorudzbinu, Porudzbina>();
        CreateMap<DTOArtikliIPorudzbine, ArtikalIPorudzbina>().ReverseMap();
    
        CreateMap<DTODodajArtikalIPorudzbina, ArtikalIPorudzbina>();
        CreateMap<Porudzbina, DTOPorudzbina>().ReverseMap();
 
        CreateMap<DTOAzuriranjeKorisnika, Korisnik>();
        CreateMap<DTODodajArtikal, Artikal>();


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