using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.SecurityTokenService;

using Org.BouncyCastle.Crypto.Generators;
using Raven.Client.Exceptions;
using System.Text;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;
using static Azure.Core.HttpHeader;
using Web_Projekat_PR111_2019.Exceptions;
using Web_Projekat_PR111_2019.Repositories;
using Web_Projekat_PR111_2019.Data;
using Microsoft.EntityFrameworkCore;
using Octokit;

namespace Web_Projekat_PR111_2019.Services
{
    public class KorisnikServis:IKorisnikServis
    {
        private readonly IKorisnikRepository repozitorijumKorisnik;
        private readonly IMapper mapper;
        private readonly IConfiguration konfiguracija;
        private readonly IEmailServis emailServis;
       

        public KorisnikServis(IKorisnikRepository repKorisnik, IEmailServis emailService, IMapper mapper_, IConfiguration konfiguracija_)
        {
            repozitorijumKorisnik = repKorisnik;
            emailServis = emailService;
            mapper = mapper_;
            konfiguracija = konfiguracija_;
        }
        public async Task<DTOKorisnik> GetById(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new Exceptions.NotFoundException($"User with ID: {id} doesn't exist.");
            }

            DTOKorisnik DtoK = mapper.Map<Korisnik, DTOKorisnik>(korisnik);
            return DtoK;
        }

        

        public async Task<List<DTOKorisnik>> GetSviKorisnici()
        {
            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviKorisnici();

            if (korisnici.Count == 0)
            {
                throw new Exceptions.NotFoundException($"There are no users!");
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
                throw new Exceptions.NotFoundException($"There are no salesmans!");
            }

            return prodavci;
        }

        public async Task<DTOKorisnik> Registracija(DTORegistracija DtoReg)
        {
            if (String.IsNullOrEmpty(DtoReg.Ime) || String.IsNullOrEmpty(DtoReg.Prezime) || String.IsNullOrEmpty(DtoReg.KorisnickoIme) ||
        String.IsNullOrEmpty(DtoReg.Email) || String.IsNullOrEmpty(DtoReg.Adresa) ||
        String.IsNullOrEmpty(DtoReg.Lozinka) || String.IsNullOrEmpty(DtoReg.RepeatLozinka) || String.IsNullOrEmpty(DtoReg.TipKorisnika.ToString()))
            {
                throw new Exception($"You must fill in all fields for registration!");
            }

            List<Korisnik> korisnici  = await repozitorijumKorisnik.GetSviKorisnici();

            if (korisnici.Any(k => k.KorisnickoIme == DtoReg.KorisnickoIme))
            {
                throw new Exceptions.ConflictException("Username already in use. Try again!");
            }

            if (korisnici.Any(u => u.Email == DtoReg.Email))
            {
                throw new Exceptions.ConflictException("Email already in use. Try again!");
            }

            if (DtoReg.Lozinka != DtoReg.RepeatLozinka)
            {
                throw new Exceptions.BadRequestException("Passwords do not match. Try again!");
            }

            Korisnik noviKorisnik = mapper.Map<DTORegistracija, Korisnik>(DtoReg);
            noviKorisnik.Slika = Encoding.ASCII.GetBytes(DtoReg.Slika);
            noviKorisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(noviKorisnik.Lozinka);

            if (noviKorisnik.TipKorisnika == Models.TipKorisnika.Prodavac)
            {
                noviKorisnik.StatusKorisnika = Models.StatusKorisnika.UObradi;
            }
            else
            {
                noviKorisnik.StatusKorisnika = Models.StatusKorisnika.Verifikovan;
            }

            Korisnik registrovaniKorisnik = await repozitorijumKorisnik.Registracija(noviKorisnik);
            DTOKorisnik dtoKor = mapper.Map<Korisnik, DTOKorisnik>(registrovaniKorisnik);
            dtoKor.Slika = Encoding.Default.GetString(noviKorisnik.Slika);
            return dtoKor;
        }



        public async Task<DTOKorisnik> UpdateKorisnik(int id, DTOUpdateKorisnik DtoKorisnik)
        {
            List<Korisnik> korisnici = await repozitorijumKorisnik.GetSviKorisnici();
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
                throw new Exceptions.NotFoundException($"User with ID {id} doesn't exist!");

            if (string.IsNullOrWhiteSpace(DtoKorisnik.Ime) || string.IsNullOrWhiteSpace(DtoKorisnik.Prezime) ||
                string.IsNullOrWhiteSpace(DtoKorisnik.KorisnickoIme) || string.IsNullOrWhiteSpace(DtoKorisnik.Email) ||
                string.IsNullOrWhiteSpace(DtoKorisnik.Adresa))
                throw new Exceptions.BadRequestException("You must fill in all fields to update the profile!");

            if (DtoKorisnik.KorisnickoIme != korisnik.KorisnickoIme)
                if (korisnici.Any(k => k.KorisnickoIme == DtoKorisnik.KorisnickoIme))
                    throw new Exceptions.ConflictException("Username already in use. Try again!");

            if (DtoKorisnik.Email != korisnik.Email)
                if (korisnici.Any(k => k.Email == DtoKorisnik.Email))
                    throw new Exceptions.ConflictException("Username already in use. Try again!");

            if (!string.IsNullOrWhiteSpace(DtoKorisnik.Lozinka))
            {
                if (string.IsNullOrWhiteSpace(DtoKorisnik.StaraLozinka))
                    throw new Exceptions.BadRequestException("You must enter the old password!");

                bool isStaraLozinkaCorrect = BCrypt.Net.BCrypt.Verify(DtoKorisnik.StaraLozinka, korisnik.Lozinka);
                if (!isStaraLozinkaCorrect)
                    throw new Exceptions.BadRequestException("Old password is incorrect!");

                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(DtoKorisnik.Lozinka);
            }

            korisnik.Ime = DtoKorisnik.Ime;
            korisnik.Prezime = DtoKorisnik.Prezime;
            korisnik.KorisnickoIme = DtoKorisnik.KorisnickoIme;
            korisnik.Email = DtoKorisnik.Email;
            korisnik.Adresa = DtoKorisnik.Adresa;

            if (!string.IsNullOrWhiteSpace(DtoKorisnik.Slika))
                korisnik.Slika = Encoding.ASCII.GetBytes(DtoKorisnik.Slika);

            Korisnik updatedKorisnik = await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            DTOKorisnik dtoK = new DTOKorisnik
            {
                Id = updatedKorisnik.IdK,
                Ime = updatedKorisnik.Ime,
                Prezime = updatedKorisnik.Prezime,
                KorisnickoIme = updatedKorisnik.KorisnickoIme,
                Email = updatedKorisnik.Email,
                Adresa = updatedKorisnik.Adresa,
                Slika = Encoding.Default.GetString(updatedKorisnik.Slika)
            };

            return dtoK;
        }

       
        
        public async Task<DTOKorisnik> VerifikacijaOdbijena(int id)
        {
            Korisnik korisnik = await repozitorijumKorisnik.GetById(id);
            if (korisnik == null)
            {
                throw new Exceptions.NotFoundException($"User with ID: {id} doesn't exist.");
            }

            if (korisnik.StatusKorisnika != Models.StatusKorisnika.UObradi)
            {
                throw new Exceptions.BadRequestException($"Cannot change verification anymore!");
            }

            korisnik.StatusKorisnika = Models.StatusKorisnika.Odbijen;
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
                throw new Exceptions.NotFoundException($"User with ID: {id} doesn't exist.");
            }
            if (korisnik.StatusKorisnika != Models.StatusKorisnika.UObradi)
            {
                throw new Exceptions.BadRequestException($"Cannot change verification anymore!");
            }

            korisnik.StatusKorisnika = Models.StatusKorisnika.Verifikovan;
            await repozitorijumKorisnik.UpdateKorisnik(korisnik);

            emailServis.PosaljiEmail(korisnik.Email, korisnik.StatusKorisnika.ToString());

            return mapper.Map<Korisnik, DTOKorisnik>(korisnik);
        }
    }
}
