using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Web_Projekat_PR111_2019.Services;

namespace Web_Projekat_PR111_2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistracijaController: ControllerBase
    {
        private IRegistracijaService registracijaService;

        public RegistracijaController(IRegistracijaService registracijaService)
        {
            this.registracijaService = registracijaService;
        }

        [HttpPost("registracija")]
        [AllowAnonymous]
        public async Task<IActionResult> Registracija([FromForm] DTOFormaRegistracije regDTO)
        {
            try
            {
                await registracijaService.Registracija(regDTO);

                return Ok(string.Format("Korisnik : {0} je uspjesno registrovan na sistem!", regDTO.KorisnickoIme));
            }
            catch (Exception e)
            {
                //return BadRequest($"Greska: {e.InnerException?.Message}");
                return BadRequest(new { errors = new List<string> { e.Message } });
            }
        }
    }
}
