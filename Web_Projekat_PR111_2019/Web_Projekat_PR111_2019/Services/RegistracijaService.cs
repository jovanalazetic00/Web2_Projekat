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


namespace Web_Projekat_PR111_2019.Services
{
    public class RegistracijaService : IRegistracijaService
    {

        private readonly IMapper maper;
        private readonly IRegistracijaRepository registracijaRepository;
        private readonly DBContext dbContext;
        private readonly IConfiguration Configuration;

        public RegistracijaService(IMapper maper, IRegistracijaRepository registracijaRepozitorijum, DBContext dbContext, IConfiguration configuration)
        {
            this.maper = maper;
            this.registracijaRepository = registracijaRepozitorijum;
            this.dbContext = dbContext;
            Configuration = configuration;
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
            registracijaKorisnika.PotvrdaLozinke = HesirajLozinku(registracijaDTO.PotvrdiLozinku);
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
            if (lozinka == potvrdjenaLozinka)
            {
                return true;
            }
            else
            {
                return false;
            }
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

       
    }
}
