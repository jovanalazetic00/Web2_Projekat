using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;

namespace Web_Projekat_PR111_2019.Services
{
    public class KorisnikServis:IKorisnikServis
    {
        private readonly IKorisnikRepository repozitorijumKorisnik;
        private readonly IMapper mapper;
        private readonly IConfiguration konfiguracija;

        public KorisnikServis(IKorisnikRepository repKorisnik, IMapper mapper_, IConfiguration konfiguracija_)
        {
            repozitorijumKorisnik = repKorisnik;
            
            mapper = mapper_;
            konfiguracija = konfiguracija_;
        }
        public Task<DTOKorisnik> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOKorisnik>> GetSviKorisnici()
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOKorisnik>> GetSviProdavci()
        {
            throw new NotImplementedException();
        }

        public Task<DTOKorisnik> Registracija(DTORegistracija DtoReg)
        {
            throw new NotImplementedException();
        }

        public Task<DTOKorisnik> UpdateKorisnik(int id, DTOUpdateKorisnik DtoKorisnik)
        {
            throw new NotImplementedException();
        }

        public Task<DTOKorisnik> VerifikacijaOdbijena(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DTOKorisnik> VerifikacijaPrihvacena(int id)
        {
            throw new NotImplementedException();
        }
    }
}
