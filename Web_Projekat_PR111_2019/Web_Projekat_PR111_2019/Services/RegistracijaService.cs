using AutoMapper;
using Microsoft.Internal.VisualStudio.PlatformUI;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Web_Projekat_PR111_2019.Services
{
    public class RegistracijaService : IRegistracijaService
    {

        private readonly IMapper maper;
        private readonly IRegistracijaRepository registracijaRepository;
        private readonly DBContext dbContext;
        private readonly IConfiguration Configuration;
        private readonly IKorisnikRepository korisnikRepository;

        public RegistracijaService(IMapper maper, IRegistracijaRepository registracijaRepository, DBContext dbContext, IConfiguration configuration, IKorisnikRepository korisnikRepository)
        {
            this.maper = maper;
            this.registracijaRepository = registracijaRepository;
            this.dbContext = dbContext;
            Configuration = configuration;
            this.korisnikRepository = korisnikRepository;
        }

        public async Task PotvrdiRegistraciju(int id)
        {
            await registracijaRepository.PotvrdiRegistraciju(id);
        }

        public static IFormFile ConvertByteArrayToIFormFile(byte[] byteArray)
        {
            var memoryStream = new MemoryStream(byteArray);
            var formFile = new FormFile(memoryStream, 0, byteArray.Length, null, "file");
            return formFile;
        }

        internal static byte[] ConvertIFormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public async Task<DTOKorisnik> Registracija(DTOFormaRegistracije registracijaDTO)
        {
            Korisnik registracijaKorisnika = maper.Map<DTOFormaRegistracije, Korisnik>(registracijaDTO);
            if (registracijaDTO.Slika != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    registracijaDTO.Slika.CopyTo(memoryStream);
                    var slikaBajti = memoryStream.ToArray();
                    registracijaKorisnika.Slika = slikaBajti;
                }
            }

            registracijaKorisnika.Lozinka = HesirajLozinku(registracijaDTO.Lozinka);
            registracijaKorisnika.PotvrdaLozinke = HesirajLozinku(registracijaDTO.PotvrdaLozinke);
            registracijaKorisnika.Obrisan = false;
            registracijaKorisnika.PotvrdaRegistracije = false;

            if (string.IsNullOrEmpty(registracijaKorisnika.KorisnickoIme) || string.IsNullOrEmpty(registracijaKorisnika.Email) || string.IsNullOrEmpty(registracijaKorisnika.Lozinka) || string.IsNullOrEmpty(registracijaKorisnika.PotvrdaLozinke) || string.IsNullOrEmpty(registracijaKorisnika.Ime) || string.IsNullOrEmpty(registracijaKorisnika.Prezime) || string.IsNullOrEmpty(registracijaKorisnika.DatumRodjenja.ToString()) || string.IsNullOrEmpty(registracijaKorisnika.Adresa) || string.IsNullOrEmpty(registracijaKorisnika.TipKorisnika.ToString()))
            {
                throw new Exception(string.Format("Sva polja moraju biti popunjena"));
            }

            if (ProvjeraLozinke(registracijaKorisnika.Lozinka, registracijaKorisnika.PotvrdaLozinke) == false)
            {

                throw new Exception(string.Format("Lozinka i potvrđena lozinka se ne poklapaju."));
            }

            int lozinka1 = registracijaKorisnika.Lozinka.Count();
            int lozinka2 = registracijaKorisnika.PotvrdaLozinke.Count();

            if (lozinka1 <= 5 || lozinka2 <= 5)
            {

                throw new Exception(string.Format("Lozinka mora sadrzati bar 5 karaktera!!"));
            }


            if (ProvjeraMail(registracijaKorisnika.Email) == false)
            {

                throw new Exception(string.Format("Neispravan unos email-a! Pokusajte ponovo."));
            }

            await registracijaRepository.Registracija(registracijaKorisnika);

            return maper.Map<DTOKorisnik>(registracijaKorisnika);
        }

        public static bool ProvjeraLozinke(string lozinka, string potvrdjenaLozinka)
        {
            return string.Equals(lozinka, potvrdjenaLozinka);
        }


        public static bool ProvjeraMail(string email)
        {
            string reg = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(reg);

            return regex.IsMatch(email);
        }

        public static string HesirajLozinku(string lozinka)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bajtoviLozinke = Encoding.UTF8.GetBytes(lozinka);
                byte[] hesiraniBajtovi = sha256.ComputeHash(bajtoviLozinke);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hesiraniBajtovi.Length; i++)
                {
                    sb.Append(hesiraniBajtovi[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

       

        public async Task<string> LogIn(DTOLogIn logovanjeDTO)
        {
            Korisnik korisnik = dbContext.Korisnici.FirstOrDefault(k => k.Email == logovanjeDTO.Email);


            if (korisnik == null)
            {
                throw new Exception(string.Format("Korisnik ne postoji! Pogrešan email ili lozinka."));
                //return Unauthorized();
            }


            string hesiranaLozinka = HesirajLozinku(logovanjeDTO.Lozinka);
            if (hesiranaLozinka != korisnik.Lozinka)
            {
                throw new Exception(string.Format("Neispravna lozinka"));
            }

            var claims = new List<Claim>
            {
                new Claim("IdKorisnika", korisnik.IdKorisnika.ToString()),
                new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString()),
                new Claim("StatusVerifikacije", korisnik.StatusVerifrikacije.ToString()),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtConfig:Key"]));

            var token = new JwtSecurityToken(
            Configuration["jwtConfig:Issuer"],
            Configuration["jwtConfig:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: new SigningCredentials(
             key, SecurityAlgorithms.HmacSha256Signature));
            //return new JwtSecurityTokenHandler().WriteToken(token);

            var tokenn = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenn;
        }

        public async Task<bool> ProvjeraEmaila(string email)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.Email == email);

            if (korisnik == null)
            {
                return false;
            }
            return true;
        }
    }
}
