using AutoMapper;
using Microsoft.AspNetCore.Http;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Services
{
    public class ArtikalService : IArtikalService
    {
        private readonly IMapper maper;
        private readonly IArtikalRepository artikalRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ArtikalService(IMapper maper, IArtikalRepository artikalRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.maper = maper;
            this.artikalRepository = artikalRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<DTOArtikal> AzurirajArtikal(int id, DTODodajArtikal artikalDTO)
        {
            var idCLaim = httpContextAccessor.HttpContext.User.FindFirst("IdKorisnika");


            if (idCLaim == null)
            {
                throw new Exception("Korisnik ne postoji u klejmu");
            }

            int idd;

            if (!int.TryParse(idCLaim.Value, out idd))
            {
                throw new Exception("Id nije konvertovan u broj");
            }
            var artikal = await artikalRepository.DobaviArtikalPoId(id);

            maper.Map(artikalDTO, artikal);

            if (artikal != null)
            {
                artikal.Cijena = artikalDTO.Cijena;
                artikal.KolicinaArtikla = artikalDTO.KolicinaArtikla;
                artikal.Naziv = artikalDTO.Naziv;
                artikal.Opis = artikalDTO.Opis;
                using (var ms = new MemoryStream())
                {
                    artikalDTO.Slika.CopyTo(ms);
                    var slikaBajti = ms.ToArray();
                    artikal.Slika = slikaBajti;
                }
            }
            else
            {
                throw new Exception(string.Format("Artikal ne postoji"));
            }


            if (artikalDTO.Naziv != artikalDTO.Naziv)
            {
                throw new Exception("Artikal sa istim nazivom postoji!");
            }

            if (artikalDTO.Cijena < 0)
            {
                throw new Exception("Cijena artikla mora biti veca od 0!");
            }
            if (artikalDTO.KolicinaArtikla < 0)
            {
                throw new Exception("Kolicina artikla mora biti veca od 0!");
            }

            
            var art = await artikalRepository.AzurirajArtikal(artikal);
            art.IdKorisnika = idd;
            return maper.Map<DTOArtikal>(art);
        }

      
        public async Task<DTOArtikal> DobaviArtikalPoId(int id)
        {
            var artikal = await artikalRepository.DobaviArtikalPoId(id);

            return maper.Map<DTOArtikal>(artikal);
        }

        public async Task<List<DTOArtikal>> DobaviSveArtikle()
        {
            var artikli = await artikalRepository.DobaviSveArtikle();
            return maper.Map<List<DTOArtikal>>(artikli);
        }

        public async Task<List<DTOArtikal>> DobaviSveArtikleOdProdavca(int idProdavca)
        {
            var artikliProdavca = await artikalRepository.DobaviArtikleProdavca(idProdavca);

            if (artikliProdavca == null)
            {
                throw new Exception("Prodavac nema artikle");
            }

            return maper.Map<List<DTOArtikal>>(artikliProdavca);
        }

        public async Task DodajArtikal(DTODodajArtikal artikalDTO)
        {
            var idCLaim = httpContextAccessor.HttpContext.User.FindFirst("IdKorisnika");


            if (idCLaim == null)
            {
                throw new Exception("Korisnik ne postoji u klejmu");
            }
            int id;

            if (!int.TryParse(idCLaim.Value, out id))
            {
                throw new Exception("Id nije konvertovan u broj");
            }


            if (string.IsNullOrEmpty(artikalDTO.Naziv))
            {
                throw new Exception("Naziv artikla je obavezan");
            }


            if (artikalDTO.Cijena <= 0)
            {
                throw new Exception("Cijena artikla mora biti veća od 0");
            }


            var artikal = maper.Map<Artikal>(artikalDTO);
            using (var memory = new MemoryStream())
            {
                artikalDTO.Slika.CopyTo(memory);
                var slikaBajti = memory.ToArray();
                artikal.Slika = slikaBajti;
            }

            artikal.IdKorisnika = id;
            await artikalRepository.DodajArtikal(artikal);

        }

        public async Task<bool> DostupanArtikal(DTOArtikal artikalDTO)
        {
            var artikal = maper.Map<Artikal>(artikalDTO);
            return await artikalRepository.ArtikalDostupan(artikal);
        }

        public async Task<DTOArtikal> ObrisiArtikal(int id)
        {
            var artikal = await artikalRepository.ObrisiArtikal(id);
            return maper.Map<DTOArtikal>(artikal);
        }
    }
}
