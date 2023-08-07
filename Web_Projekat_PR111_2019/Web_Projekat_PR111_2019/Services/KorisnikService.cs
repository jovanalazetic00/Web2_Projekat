using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper maper;

        public KorisnikService(IKorisnikRepository korisnikRepository, IMapper maper)
        {
            this.korisnikRepository = korisnikRepository;
            this.maper = maper;
        }

        public async Task<DTOKorisnik> AzurirajKorisnika(int id, DTOAzuriranjeKorisnika korisnikDto)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);
            

            if (korisnik != null)
            {
                korisnik.Ime = korisnikDto.Ime;
                korisnik.Prezime = korisnikDto.Prezime;
                korisnik.KorisnickoIme = korisnikDto.KorisnickoIme;
                korisnik.Email = korisnikDto.Email;
                korisnik.Lozinka = HesirajLozinku(korisnikDto.Lozinka);
                korisnik.DatumRodjenja = korisnikDto.DatumRodjenja;
                korisnik.Adresa = korisnikDto.Adresa;
                if (korisnikDto.Slika != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        korisnikDto.Slika.CopyTo(memoryStream);
                        var slikaBajti = memoryStream.ToArray();
                        korisnik.Slika = slikaBajti;
                    }
                }

            }
            else
            {
                throw new Exception(string.Format("Korisnik ne postoji u bazi"));
            }


            if (korisnikDto.KorisnickoIme != korisnik.KorisnickoIme)
            {
                throw new Exception("Korisnik sa datim korisničkim imenom već postoji u bazi");
            }
            if (korisnikDto.Email != korisnik.Email)
            {
                throw new Exception("Korisnik sa datim korisničkim imenom već postoji u bazi");
            }



            if (!string.IsNullOrEmpty(korisnikDto.Lozinka))
            {
                string hes = HesirajLozinku(korisnikDto.Lozinka);
                korisnik.Lozinka = hes;
            }
            if (korisnikDto.Lozinka.Length < 5)
            {
                throw new Exception(string.Format("Lozinka mora sadrzati bar 5 karaktera!!"));
            }


            var kor = await korisnikRepository.AzurirajKorisnika(korisnik);

            return maper.Map<DTOKorisnik>(kor);

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

        public async Task<Korisnik> DobaviKorisnikapoID(int id)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);

            if (korisnik == null)
            {
                throw new Exception("Korisnik  ne postoji u bazi");
            }

            
            return korisnik;
        }

        public async Task<List<DTOKorisnik>> DobaviKorisnike()
        {
            var korisnici = await korisnikRepository.DobaviKorisnike();
            if (korisnici == null)
            {
                throw new Exception("Nema korisnika u bazi");
            }

            return maper.Map<List<DTOKorisnik>>(korisnici);
        }

        public async Task<DTOKorisnik> ObrisiKorisnika(int id)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);

            if (korisnik == null)
            {
                throw new Exception("Korisnik ne postoji - ne moze se obrisati!");
            }


            var obrisani = await korisnikRepository.ObrisiKorisnika(id);

            return maper.Map<DTOKorisnik>(obrisani);
        }

        public async Task PotvrdiRegistraciju(int id)
        {
            await korisnikRepository.PotvrdiRegistraciju(id);
        }

        public async Task OdbijRegistraciju(int id)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);
            if (korisnik != null && korisnik.Verifikovan == false)
            {
                await korisnikRepository.ObrisiKorisnika(id);

            }
        }

        public async Task VerifikacijaProdavca(int id)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);


            if (korisnik == null)
            {
                throw new Exception(string.Format($"Korisnik  ne postoji u bazi"));
            }

            await korisnikRepository.VerifikacijaProdavca(id);
        }

        public async Task OdbijVerifikaciju(int id)
        {
            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);

            if (korisnik == null)
            {
                throw new Exception(string.Format($"Korisnik ne postoji u bazi"));
            }

            await korisnikRepository.OdbijVerifikaciju(id);
        }

        public async Task<List<Korisnik>> DobaviSveProdavce()
        {
            return await korisnikRepository.DobaviSveProdavce();
        }

        public async Task<List<Korisnik>> DobaviSveVerifikovaneProdavce()
        {
            var prodavci = await DobaviSveProdavce();

            List<Korisnik> verifikovani = new List<Korisnik>();

            foreach (var prodavac in prodavci)
            {
                if (prodavac.StatusVerifrikacije == StatusVerifikacije.Verifikovan && prodavac.Verifikovan == true)
                {
                    verifikovani.Add(prodavac);
                }
            }

            return verifikovani;
        }

        public async Task<List<Korisnik>> DobaviKorisnikeKojiCekajuNaVerifikaciju()
        {
            return await korisnikRepository.DobaviKorisnikeKojiCekajuNaVerifikaciju();
        }
    }
}
