using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Controllers
{
    public class KorisnikController:ControllerBase
    {
        private readonly IKorisnikServis korisnikServis;
        public KorisnikController(IKorisnikServis kServis)
        {
            korisnikServis = kServis;
        }
        [HttpGet("GetSviKorisnici")]
        [Authorize(Roles="Administartor")]
        public async Task<IActionResult> GetSviKorisnici()
        {
            try
            {
                List<DTOKorisnik> korisnici = await korisnikServis.GetSviKorisnici();

                if (korisnici.Count == 0)
                {
                    return NotFound(); // Vraća 404 Not Found ako nema korisnika
                }

                return Ok(korisnici); // Vraća listu korisnika ako postoji
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // Vraća 500 Internal Server Error ako dođe do greške
            }
        }

        [HttpGet("GetSviProdavci")]
        [Authorize(Roles = "Administartor")]
        public async Task<IActionResult> GetSviProdavci()
        {
            try
            {
                List<DTOKorisnik> prodavci = await korisnikServis.GetSviProdavci();

                if (prodavci.Any())
                {
                    return Ok(prodavci); // Vraća listu prodavaca ako postoji
                }

                return NoContent(); // Vraća 204 No Content ako nema prodavaca
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // Vraća 500 Internal Server Error ako dođe do greške
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(DTOUpdateKorisnik DtoK)
        {
            try
            {
                int id = int.Parse(User.Claims.First(k => k.Type == "IDKorisnika").Value);
                DTOKorisnik korisnik = await korisnikServis.UpdateKorisnik(id, DtoK);

                if (korisnik != null)
                {
                    return Ok(korisnik); // Vraća ažuriranog korisnika ako je ažuriranje uspešno
                }

                return NotFound(); // Vraća 404 Not Found ako korisnik nije pronađen ili ažuriranje nije uspelo
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // Vraća 500 Internal Server Error ako dođe do greške
            }
        }

        public async Task<IActionResult> Post([FromBody] DTORegistracija DtoReg)
        {
            try
            {
                DTOKorisnik korisnik = await korisnikServis.Registracija(DtoReg);

                if (korisnik != null)
                {
                    return Ok(korisnik);
                }

                return BadRequest(); // Vraća 400 Bad Request ako registracija nije uspela
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}"); // Vraća 500 Internal Server Error ako dođe do greške
            }
        }

        [HttpPut("VerifikacijaPrihvacena/id")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> VerifikacijaPrihvacena(int id)
        {
            try
            {
                DTOKorisnik korisnik = await korisnikServis.VerifikacijaPrihvacena(id);
                if (korisnik == null)
                {
                    return BadRequest("Korisnik sa datim ID-jem nije pronađen.");
                }
                return Ok(korisnik);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Greška prilikom prihvatanja verifikacije: {e.Message}");
            }
        }

        [HttpPut("VerifikacijaOdbijena/id")]
        [Authorize(Roles = "Administartor")]
        public async Task<IActionResult> VerifikacijaOdbijena(int id)
        {
            try
            {
                DTOKorisnik korisnik = await korisnikServis.VerifikacijaOdbijena(id);
                if (korisnik == null)
                {
                    return BadRequest("Korisnik sa datim ID-jem nije pronađen.");
                }
                return Ok(korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom odbijanja verifikacije: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("GetMojProfil")]
        public async Task<IActionResult> GetMojProfil()
        {
            try
            {
                int id = int.Parse(User.Claims.First(u => u.Type == "KorisnikId").Value);
                DTOKorisnik korisnik = await korisnikServis.GetById(id);
                if (korisnik == null)
                {
                    return BadRequest("Korisnik nije pronađen.");
                }
                return Ok(korisnik);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja korisničkog profila: {e.Message}");
            }
        }

    }
}
