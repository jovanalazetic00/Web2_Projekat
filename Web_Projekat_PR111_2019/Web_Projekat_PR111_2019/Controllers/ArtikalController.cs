using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Services;

namespace Web_Projekat_PR111_2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            this.artikalService = artikalService;
        }

        [HttpGet("dobaviArtikle")]
        [Authorize(Roles = "Kupac")]
        public async Task<ActionResult<List<DTOArtikal>>> DobaviArtikle()
        {
            try
            {
                var artikli = await artikalService.DobaviSveArtikle();
                return Ok(artikli);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("dobaviArtikal/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<ActionResult<DTOArtikal>> DobaviArtikal(int id)
        {
            try
            {
                var artikal = await artikalService.DobaviArtikalPoId(id);

                if (artikal == null)
                {
                    return BadRequest("Artikal ne postoji");
                }
                return Ok(artikal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("azurirajArtikal/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<ActionResult<DTOArtikal>> AzurirajArikal(int id, [FromForm] DTODodajArtikal artikalDTO)
        {
            try
            {
                var artikal = await artikalService.AzurirajArtikal(id, artikalDTO);
                if (artikal == null)
                {
                    return BadRequest("Artikal ne postoji");
                }
                return Ok(artikal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("obrisiArtikal/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<ActionResult<DTOArtikal>> ObrisiArtikal(int id)
        {
            try
            {
                var obrisanArtikal = await artikalService.ObrisiArtikal(id);
                if (obrisanArtikal == null)
                {
                    return BadRequest("Artikal ne postoji");
                }
                return Ok(obrisanArtikal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("dodajArtikal")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> DodajArtikal([FromForm] DTODodajArtikal artikalDTO)
        {
            try
            {
                await artikalService.DodajArtikal(artikalDTO);

                return Ok(string.Format("Artikal : {0} je uspjesno dodat u bazu!", artikalDTO.Naziv));
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }

        }


        [HttpGet("artikalDostupan")]
        [Authorize(Roles = "Prodavac")]
        public async Task<ActionResult<bool>> ArtikalDostupan(DTOArtikal artikalDTO)
        {
            try
            {
                var dostupan = await artikalService.DostupanArtikal(artikalDTO);
                return Ok(dostupan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("dobaviArtikleProdavca/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<ActionResult<List<DTOArtikal>>> DobaviArtikleProdavca(int id)
        {
            try
            {
                var artikli = await artikalService.DobaviSveArtikleOdProdavca(id);
                return Ok(artikli);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
