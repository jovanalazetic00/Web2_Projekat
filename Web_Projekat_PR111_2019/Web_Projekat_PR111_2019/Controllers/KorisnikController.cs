using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Web_Projekat_PR111_2019.Services;

namespace Web_Projekat_PR111_2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KorisnikController: ControllerBase
    {
        private readonly IKorisnikService korisnikService;
        private readonly IEmailService emailService;

       
        public KorisnikController(IKorisnikService korisnikService, IEmailService emailService)
        {
            this.korisnikService = korisnikService;
            this.emailService = emailService;
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DTOKorisnik>> BrisanjeKorisnika(int id)
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

        [HttpGet("dobaviKorisnikaPoId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DobaviKorisnikaPoId(int id)
        {
            try
            {
                var korisnik = await korisnikService.DobaviKorisnikaPoId(id);

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
        [Authorize(Roles = "Administrator")]
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


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<DTOKorisnik>> AzurirajKorisnika(int id, [FromForm] DTOAzuriranjeKorisnika korisnikDTO)
        {
            try
            {
                var azuriraniKorisnik = await korisnikService.AzurirajKorisnika(id, korisnikDTO);
                if (azuriraniKorisnik == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }
                return Ok(azuriraniKorisnik);
            }
            catch (Exception ex)
            {

                return BadRequest($"Greska: {ex.InnerException?.Message}");

            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Korisnik>> DobaviKorisnika(int id)
        {
            try
            {
                var korisnik = await korisnikService.DobaviKorisnikaPoId(id);

                if (korisnik == null)
                {
                    return BadRequest("Korisnik ne postoji");
                }
                return Ok(korisnik);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("verifikacijaProdavca/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> VerifikacijaProdavca(int id)
        {
            try
            {
                await korisnikService.VerifikacijaProdavca(id);
                return Ok(string.Format("Uspjesno izvrsena verfikacija prodavca"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<DTOKorisnik>>> DobaviKorisnike()
        {
            try
            {
                var korisnici = await korisnikService.DobaviKorisnike();
                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("odbijVerifikaciju/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> OdbijVerifikaciju(int id)
        {
            try
            {
                await korisnikService.OdbijVerifikaciju(id);
                return Ok(string.Format("Uspjesno odbijena verifikacija"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dobaviNeverifikovaneKorisnike")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<DTOKorisnik>>> DobaviNeverifikovaneKorisnike()
        {
            try
            {
                var korisnici = await korisnikService.DobaviKorisnikeKojiCekajuNaVerifikaciju();
                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("dobaviProdavce")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Korisnik>>> DobaviProdavce()
        {
            try
            {
                var korisnici = await korisnikService.DobaviSveProdavce();
                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("dobaviVerifikovaneProdavce")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Korisnik>>> DobaviVerifikovaneProdavce()
        {
            try
            {
                var korisnici = await korisnikService.DobaviSveVerifikovaneProdavce();
                return Ok(korisnici);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/profil")]
        [AllowAnonymous]
        public async Task<IActionResult> Profil(int id)
        {
            try
            {
                var korisnik = await korisnikService.DobaviKorisnikaPoId(id);

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

        [HttpPost]
        public async Task<IActionResult> Posalji(string poruka, string verifikacija)
        {
            try
            {
                emailService.SlanjePoruke(poruka, verifikacija);
                return Ok(string.Format("Uspjesno poslata poruka"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
