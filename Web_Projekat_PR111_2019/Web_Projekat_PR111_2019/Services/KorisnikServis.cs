using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.SecurityTokenService;
using OpenQA.Selenium;
using Raven.Client.Exceptions;
using System.Text;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;
using static Azure.Core.HttpHeader;
using BadRequestException = Microsoft.IdentityModel.SecurityTokenService.BadRequestException;

namespace Web_Projekat_PR111_2019.Services
{
    public class KorisnikServis:IKorisnikServis
    {
        private readonly IKorisnikRepository repozitorijumKorisnik;
        private readonly IMapper mapper;
        private readonly IConfiguration konfiguracija;
        private readonly IEmailServis emailServis;

        public KorisnikServis(IKorisnikRepository repKorisnik, IMapper mapper_, IConfiguration konfiguracija_)
        {
            repozitorijumKorisnik = repKorisnik;
            
            mapper = mapper_;
            konfiguracija = konfiguracija_;
        }
        public async Task<DTOKorisnik> GetById(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            }

            DTOKorisnik DtoK = mapper.Map<Korisnik, DTOKorisnik>(korisnik);
            return DtoK;
        }

        public async Task<List<DTOKorisnik>> GetSviKorisnici()
        {
            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviKorisnici();

            if (korisnici.Count == 0)
            {
                throw new NotFoundException($"There are no users!");
            }

            return mapper.Map<List<Korisnik>, List<DTOKorisnik>>(korisnici);
        }

        public async Task<List<DTOKorisnik>> GetSviProdavci()
        {
            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviProdavci();

            List<DTOKorisnik> prodavci = korisnici
                .Where(k => k.TipKorisnika == TipKorisnika.Prodavac)
                .Select(k => mapper.Map<Korisnik, DTOKorisnik>(k))
                .ToList();

            if (prodavci.Count == 0)
            {
                throw new NotFoundException($"There are no salesmans!");
            }

            return prodavci;
        }

        public Task<DTOKorisnik> Registracija(DTORegistracija DtoReg)
        {
            throw new NotImplementedException();
        }

        

        public Task<DTOKorisnik> UpdateKorisnik(int id, DTOUpdateKorisnik DtoKorisnik)
        {
            throw new NotImplementedException();
        }

        public async Task<DTOKorisnik> VerifikacijaOdbijena(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            }

          /* if (korisnik.StatusKorisnika != Common.StatusKorisnika.UObradi)
            {
                throw new BadRequestException($"Cannot change verification anymore!");
            }*/

           // korisnik.Verification = Common.StatusKorisnika.Odbijen;
            Korisnik noviKorisnik = await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            if (noviKorisnik != null)
            {
                emailServis.PosaljiEmail(noviKorisnik.Email, noviKorisnik.StatusKorisnika.ToString());
            }

            DTOKorisnik korisnikDto = mapper.Map<Korisnik, DTOKorisnik>(noviKorisnik);
            return korisnikDto;
        }

        public async Task<DTOKorisnik> VerifikacijaPrihvacena(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new NotFoundException($"User with ID: {id} doesn't exist.");
            }
          /*  if (korisnik.StatusKorisnika != Common.StatusKorisnika.UObradi)
            {
                throw new BadRequestException($"Cannot change verification anymore!");
            }*/

          //  korisnik.StatusKorisnika = Common.StatusKorisnika.Prihvacen;
            await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            emailServis.PosaljiEmail(korisnik.Email, korisnik.StatusKorisnika.ToString());

            return mapper.Map<Korisnik, DTOKorisnik>(korisnik);
        }
    }
}
