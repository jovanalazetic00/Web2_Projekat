using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Services
{
    public class PorudzbinaService : IPorudzbinaService
    {
        private readonly IMapper maper;
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IArtikalRepository artikalRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly DBContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IArtikalIPorudzbinaRepository artPorRepository;

        public PorudzbinaService(IMapper maper, IPorudzbinaRepository porudzbinaRepository, IArtikalRepository artikalRepository, IKorisnikRepository korisnikRepository, DBContext dbContext, IHttpContextAccessor httpContextAccessor, IArtikalIPorudzbinaRepository artPorRepository)
        {
            this.maper = maper;
            this.porudzbinaRepository = porudzbinaRepository;
            this.artikalRepository = artikalRepository;
            this.korisnikRepository = korisnikRepository;
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.artPorRepository = artPorRepository;
        }

        public async Task<DTOPorudzbina> AzurirajPorudzbinu(int id, DTOPorudzbina porudzbinaDTO)
        {
            var porudzbina = await porudzbinaRepository.DobaviPorudzbinuPoId(id);


            if (porudzbina != null)
            {
                porudzbina.AdresaIsporuke = porudzbinaDTO.AdresaIsporuke;
                porudzbina.KomentarPorudzbine = porudzbinaDTO.KomentarPorudzbine;
            }
            else
            {
                throw new Exception(string.Format("Porudzbina ne postoji u bazi"));
            }


            if (string.IsNullOrEmpty(porudzbinaDTO.AdresaIsporuke))
            {
                throw new Exception("Polje adresa je obavezno!");
            }

            var por = await porudzbinaRepository.AzurirajPorudzbinu(porudzbina);


            return maper.Map<DTOPorudzbina>(por);
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbine(int idPorudzbine)
        {
            var artikli = await porudzbinaRepository.DobaviArtikleOdPorudzbine(idPorudzbine);

            if (artikli == null)
            {
                throw new Exception("Porudzbina nema nijedan artikal");
            }

            return artikli;
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbineProdavca(int idPorudzbine)
        {
            var idClaim = httpContextAccessor.HttpContext.User.FindFirst("IdKorisnika");

            if (idClaim == null)
            {
                throw new Exception("Korisnik ne postoji u klajmu");
            }

            int id;
            if (!int.TryParse(idClaim.Value, out id))
            {
                throw new Exception("ID nije konvertovan u broj");
            }

            var artikli = await porudzbinaRepository.DobaviArtiklePorudzbineProdavca(idPorudzbine, id);

            if (artikli == null)
            {
                throw new Exception("Porudzbina nema artikle");
            }

            return artikli;
        }

        public async Task<Porudzbina> DobaviPorudzbinuPoId(int id)
        {
            var porudzbina = await porudzbinaRepository.DobaviPorudzbinuPoId(id);
            if (porudzbina == null)
            {
                throw new Exception("Ne postoji u bazi");
            }

            return porudzbina;
        }

        public async Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int id)
        {
            var porudzbine = await porudzbinaRepository.DobaviPrethodnePorudzbineKupca(id);
            if (porudzbine == null)
            {
                throw new Exception("Nema porudzbina u bazi");
            }
            return porudzbine;
        }

        public async Task<List<Porudzbina>> DobaviSvePorudzbine()
        {
            var porudzbine = await porudzbinaRepository.DobaviSvePorudzbine();
            if (porudzbine == null)
            {
                throw new Exception("Nema porudzbina u bazi");
            }
            return porudzbine;
        }

        public async Task<List<Porudzbina>> DobaviSvePorudzbineKupca(int kupacId)
        {
            var svePorudzbine = await porudzbinaRepository.DobaviSvePorudzbine();

            List<Porudzbina> porudzbineKupca = new List<Porudzbina>();

            foreach (var por in svePorudzbine)
            {
                if (por.IdKorisnika == kupacId)
                {
                    porudzbineKupca.Add(por);
                }
            }

            return porudzbineKupca;
        }

        public async Task<int> DodajPoruzbinu(DTODodajPorudzbinu porudzbinaDTO)
        {
            var idClaim = httpContextAccessor.HttpContext.User.FindFirst("IdKorisnika");

            if (idClaim == null)
            {
                throw new Exception("Korisnik ne postoji u klajmu");
            }

            int id;
            if (!int.TryParse(idClaim.Value, out id))
            {
                throw new Exception("ID nije konvertovan u broj");
            }

            if (string.IsNullOrEmpty(porudzbinaDTO.AdresaIsporuke))
            {
                throw new Exception("Polje adresa je obavezno!");
            }

            var korisnik = await korisnikRepository.DobaviKorisnikaPoId(id);

            var porudzbina = maper.Map<Porudzbina>(porudzbinaDTO);

            porudzbina.IdKorisnika = id;
            porudzbina.StatusPorudzbine = StatusPorudzbine.UObradi;
            porudzbina.VrijemePorudzbine = DateTime.Now;
            porudzbina.VrijemeIsporuke = DateTime.Now.AddHours(1).AddMinutes(new Random().Next(60));
            porudzbina.Korisnik = korisnik;


            foreach (var stavkaDTO in porudzbinaDTO.Stavke)
            {
                var artikal = await artikalRepository.DobaviArtikalPoId(stavkaDTO.IdArtikalIPorudzbina);

                if (stavkaDTO.Kolicina > artikal.KolicinaArtikla)
                {
                    throw new Exception("Nedovoljna kolicina artikla na stanju");
                }

                var stavka1 = new ArtikalIPorudzbina
                {
                    IDPorudzbineAIP = porudzbina.IdPorudzbine,
                    IDArtiklaAIP = artikal.ArtikalId,
                    Porudzbina = porudzbina,
                    Artikal = artikal,
                    KolicinaArtikla = stavkaDTO.Kolicina
                };

                var existingStavka = porudzbina.ArtikliIPorudzbine.FirstOrDefault(s => s.IDPorudzbineAIP == stavka1.IDPorudzbineAIP && s.IDArtiklaAIP == stavka1.IDArtiklaAIP);

                if (existingStavka == null)
                {

                    try
                    {
                        porudzbina.ArtikliIPorudzbine.Add(stavka1);
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            string s = e.InnerException.Message;
                        }
                    }
                }

                //300 je cijena dostave
                artikal.KolicinaArtikla -= stavkaDTO.Kolicina;
                porudzbina.CijenaPorudzbine += stavkaDTO.Kolicina * artikal.Cijena + 300;

            }

            await porudzbinaRepository.DodajPoruzbinu(porudzbina);
            return porudzbina.IdPorudzbine;
        }

        public async Task<List<Porudzbina>> MojePorudzbine(int id)
        {
            var svePorudzbine = await porudzbinaRepository.MojePorudzbine(id);


            var mojePorudzbine = new List<Porudzbina>();
            DateTime trenutnoVrijeme = DateTime.Now;


            foreach (var por in svePorudzbine)
            {
                var stavkePorudzbine = await artPorRepository.DobaviStavkePorudzbine(por.IdPorudzbine);

                foreach (var stavka in stavkePorudzbine)
                {
                    var artikal = await dbContext.Artikli
                        .FirstOrDefaultAsync(a => a.ArtikalId == stavka.IDArtiklaAIP && a.IdKorisnika == id);

                    if (artikal != null && (trenutnoVrijeme - por.VrijemePorudzbine).TotalSeconds >= 3600)
                    {
                        mojePorudzbine.Add(por);
                        break;
                    }
                }
            }

            return mojePorudzbine;
        }

        public async Task<List<Porudzbina>> NovePorudzineProdavca(int id)
        {
            var svePorudzbine = await porudzbinaRepository.NovePorudzineOdProdavca(id);

            var novePorProdavca = new List<Porudzbina>();
            DateTime trenutnoVrijeme = DateTime.Now;

            foreach (var por in svePorudzbine)
            {
                var stavkePorudzbine = await artPorRepository.DobaviStavkePorudzbine(por.IdPorudzbine);


                foreach (var stavka in stavkePorudzbine)
                {
                    var artikal = await dbContext.Artikli
                        .FirstOrDefaultAsync(a => a.ArtikalId == stavka.IDArtiklaAIP && a.IdKorisnika == id);


                    if (artikal != null && (trenutnoVrijeme - por.VrijemePorudzbine).TotalSeconds < 3600)
                    {
                        novePorProdavca.Add(por);
                        break;
                    }
                }
            }

            return novePorProdavca;
        }

        public async Task<DTOPorudzbina> ObrisiPorudzbina(int id)
        {
            var por = await porudzbinaRepository.ObrisiPorudzbinu(id);
            return maper.Map<DTOPorudzbina>(por);
        }

        public async Task OtkaziPorudzbinu(int id)
        {
            var porudzbina = await porudzbinaRepository.DobaviPorudzbinuPoId(id);

            DateTime sada = DateTime.Now;
            DateTime vrijemePorucivanja = porudzbina.VrijemePorudzbine;
            TimeSpan sat = TimeSpan.FromHours(1);

            if (sada - vrijemePorucivanja >= sat)
            {
                throw new Exception("Prosao je jedan sat od porucivanja. Ne mozete otkazati porudzbinu!");
            }

            await porudzbinaRepository.OtkazivanjePorudzbinu(id);
        }
    }
}
