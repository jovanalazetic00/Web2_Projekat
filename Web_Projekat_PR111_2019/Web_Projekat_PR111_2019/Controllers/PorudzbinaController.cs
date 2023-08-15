using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Web_Projekat_PR111_2019.Services;

namespace Web_Projekat_PR111_2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("ReactAppPolicy")]
    public class PorudzbinaController:ControllerBase
    {
        private readonly IPorudzbinaService porudzbinaService;

        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            this.porudzbinaService = porudzbinaService;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<Porudzbina>>> DobaviSvePorudzbine()
        {
            try
            {
                var porudzbine = await porudzbinaService.DobaviSvePorudzbine();
                return Ok(porudzbine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<ActionResult<Porudzbina>> DobaviPorudzbinu(int id)
        {
            try
            {
                var porudzbina = await porudzbinaService.DobaviPorudzbinuPoId(id);

                if (porudzbina == null)
                {
                    return BadRequest("Porudzbina ne postoji");
                }
                return Ok(porudzbina);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<ActionResult<DTOPorudzbina>> AzurirajPorudzbinu(int id, DTOPorudzbina porudzbinaDTO)
        {
            try
            {
                var porudzbina = await porudzbinaService.AzurirajPorudzbinu(id, porudzbinaDTO);

                if (porudzbina == null)
                {
                    return BadRequest("Artikal ne postoji");
                }
                return Ok(porudzbina);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<ActionResult<DTOPorudzbina>> ObrisiPorudzbinu(int id)
        {
            try
            {
                var por = await porudzbinaService.ObrisiPorudzbina(id);

                if (por == null)
                {
                    return BadRequest("Porudzbina ne postoji");
                }
                return Ok(por);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("dodajPorudzbinu")]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> DodajPorudzbinu([FromBody] DTODodajPorudzbinu porudzbinaDTO)
        {
            try
            {
                int idPorudzbine = await porudzbinaService.DodajPoruzbinu(porudzbinaDTO);

                return Ok(idPorudzbine);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException?.Message ?? e.Message);
            }

        }

        [HttpGet("prethodnePorudzbine/{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<ActionResult<Porudzbina>> PrethodnePorudzbine(int id)
        {
            try
            {
                var prethodne = await porudzbinaService.DobaviPrethodnePorudzbineKupca(id);

                if (prethodne == null)
                {
                    return BadRequest("Porudzbina ne postoji");
                }
                return Ok(prethodne);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpPut("otkaziPorudzbinu/{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> OtkaziPorudzbinu(int id)
        {
            try
            {
                await porudzbinaService.OtkaziPorudzbinu(id);

                return Ok("otkazana");

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("porudzbineKupca/{id}")]
        [Authorize(Roles = "Kupac")]
        public async Task<IActionResult> PorudzbineKupca(int id)
        {
            try
            {
                var porudzbine = await porudzbinaService.DobaviPrethodnePorudzbineKupca(id);

                return Ok(porudzbine);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("novePorudzbineProdavca/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> NovePorudzbineProdavca(int id)
        {
            try
            {
                var porudzbine = await porudzbinaService.NovePorudzineProdavca(id);

                return Ok(porudzbine);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



        [HttpGet("mojePorudzbineProdavac/{id}")]
        [Authorize(Roles = "Prodavac")]
        public async Task<IActionResult> MojePorudzbine(int id)
        {
            try
            {
                var porudzbine = await porudzbinaService.MojePorudzbine(id);

                return Ok(porudzbine);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("dobaviArtiklePorudzbine/{id}")]
        [Authorize(Roles = "Administrator, Kupac")]
        public async Task<ActionResult<List<Artikal>>> DobaviArtiklePorudzbine(int id)
        {
            try
            {
                var artikli = await porudzbinaService.DobaviArtiklePorudzbine(id);
                return Ok(artikli);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("dobaviArtiklePorudzbineProdavca/{id}")]
        [Authorize(Roles = "Prodavac")]

        public async Task<ActionResult<List<Artikal>>> DobaviArtiklePorudzbineProdavca(int id)
        {
            try
            {
                var artikli = await porudzbinaService.DobaviArtiklePorudzbineProdavca(id);
                return Ok(artikli);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
