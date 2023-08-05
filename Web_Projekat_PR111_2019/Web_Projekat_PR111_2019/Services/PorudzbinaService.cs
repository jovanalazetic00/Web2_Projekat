using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public PorudzbinaService(IMapper maper, IPorudzbinaRepository porudzbinaRepository, IArtikalRepository artikalRepository, IKorisnikRepository korisnikRepository, DBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.maper = maper;
            this.porudzbinaRepository = porudzbinaRepository;
            this.artikalRepository = artikalRepository;
            this.korisnikRepository = korisnikRepository;
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
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

        public async Task<Porudzbina> DobaviPorudzbinuPoId(int id)
        {
            var porudzbina = await porudzbinaRepository.DobaviPorudzbinuPoId(id);
            if (porudzbina == null)
            {
                throw new Exception("Ne postoji u bazi");
            }

            return porudzbina;
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

            if (string.IsNullOrEmpty(porudzbinaDTO.Adresa))
            {
                throw new Exception("Polje adresa je obavezno!");
            }

            var korisnik = await korisnikRepository.DobaviKorisnikaPoID(id);

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
                    IDPorudzbine = porudzbina.IdPorudzbine,
                    IDArtikla = artikal.ArtikalId,
                    Porudzbina = porudzbina,
                    Artikal = artikal,
                    KolicinaArtikla = stavkaDTO.Kolicina
                };

                var existingStavka = porudzbina.ArtikliIPorudzbine.FirstOrDefault(s => s.IDPorudzbine == stavka1.IDPorudzbine && s.IDArtikla == stavka1.IDArtikla);

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
    

        public async Task<DTOPorudzbina> ObrisiPorudzbina(int id)
        {
            var por = await porudzbinaRepository.ObrisiPorudzbinu(id);
            return maper.Map<DTOPorudzbina>(por);
        }
    }
}
