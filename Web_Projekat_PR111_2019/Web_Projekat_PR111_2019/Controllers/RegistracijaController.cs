using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces.IServices;
using Web_Projekat_PR111_2019.Models;
using Web_Projekat_PR111_2019.Services;
using System.Collections.Generic;

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


        [HttpPost("logovanje")]
        [AllowAnonymous]
        public async Task<IActionResult> Logovanje([FromBody] DTOLogIn logDTO)
        {
            try
            {
                string token = await registracijaService.LogIn(logDTO);

                return Ok(string.Format("{0}", token));
            }
            catch (Exception e)
            {
                return BadRequest(new { errors = new List<string> { e.Message } });
            }
        }

        [HttpPost("potvrdiRegistraciju/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PotvrdiRegistraciju(int id)
        {
            try
            {
                await registracijaService.PotvrdiRegistraciju(id);

                return Ok("Registracija je uspješno potvrđena.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("provjeraEmaila/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> ProvjeraEmaila(string email)
        {
            var rezultat = await registracijaService.ProvjeraEmaila(email);

            if (rezultat)
            {
                return Ok(true);
            }
            else
            {
                return NotFound(false);
            }
        }
    }
}
