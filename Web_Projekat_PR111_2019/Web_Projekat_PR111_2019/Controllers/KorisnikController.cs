using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Services;

namespace Web_Projekat_PR111_2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController: ControllerBase
    {
        private readonly IKorisnikService korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            this.korisnikService = korisnikService;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DTOKorisnik>> BrisanjeKorisnik(int id)
        {
            try
            {
                var obrisanKorisnik = await korisnikService.ObrisiKorisnika(id);
                if (obrisanKorisnik == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }
                return Ok(obrisanKorisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/dobaviKorisnikaPoId")]
        [AllowAnonymous]
        public async Task<IActionResult> DobaviKorisnikaPoId(int id)
        {
            try
            {
                var korisnik = await korisnikService.DobaviKorisnikapoID(id);

                if (korisnik == null)
                {
                    throw new Exception("Korisnik ne postoji u bazi");
                }

                return Ok(korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("potvrdaRegistracije/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PotvrdaRegistracije(int id)
        {
            try
            {
                await korisnikService.PotvrdiRegistraciju(id);
                return Ok(string.Format("Uspjesno izvrsena potvrda registracije  korisnika sa id-jem {0}", id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("odbijRegistraciju/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> OdbijRegistraciju(int id)
        {
            try
            {
                await korisnikService.OdbijRegistraciju(id);
                return Ok(string.Format("Odbijena registracija korisnika sa id {0}. Korisnik je obrisan iz baze!", id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
