using Microsoft.AspNetCore.Authorization;
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
    public class PorudzbinaController:ControllerBase
    {
        private readonly IPorudzbinaService porudzbinaService;

        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            this.porudzbinaService = porudzbinaService;
        }

        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
    }
}
