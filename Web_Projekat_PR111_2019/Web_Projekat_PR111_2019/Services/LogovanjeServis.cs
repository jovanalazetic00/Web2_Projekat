using AutoMapper;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Octokit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Services
{
    public class LogovanjeServis : ILogovanjeServis
    {
        private readonly IMapper mapper;
        private readonly IKorisnikRepository korisnikRepozitorijum;
        private readonly IConfiguration konfiguracija;

        public LogovanjeServis(IKorisnikRepository korisnikRepozitorijum_, IMapper mapper_, IConfiguration konfiguracija_)
        {
            korisnikRepozitorijum = korisnikRepozitorijum_;
            mapper = mapper_;
            konfiguracija = konfiguracija_;
        }
        public async Task<string> LogovanjeKorisnika(DTOLogovanje logovanjeDto)
        {
            Korisnik? korisnik = await korisnikRepozitorijum.GetKorisnikByEmail(logovanjeDto.Email);
            if (korisnik == null)
            {
                throw new Exception($"User with email {logovanjeDto.Email} doesn't exist! Try again.");
            }

            // Provera da li je uneta lozinka ispravna
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(logovanjeDto.Lozinka, korisnik.Lozinka);
            if (!isPasswordValid)
            {
                throw new Exception("Incorrect password! Try again.");
            }

            // Generisanje JWT tokena
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("KorisnikId", korisnik.IdK.ToString()),
                new Claim("Email", korisnik.Email),
                new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString()),
                new Claim("Verification", korisnik.StatusKorisnika.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(konfiguracija["Jwt:Key"] ?? "default"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                konfiguracija["Jwt:Issuer"],
                konfiguracija["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
