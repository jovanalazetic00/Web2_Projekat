using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;

namespace Web_Projekat_PR111_2019.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogovanjeController : ControllerBase
    {
        private readonly ILogovanjeServis logovanjeServis;

        public LogovanjeController(ILogovanjeServis logServis)
        {
            logovanjeServis = logServis;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Post([FromBody] DTOLogovanje logDTO)
        {
            try
            {
                string token = await logovanjeServis.LogovanjeKorisnika(logDTO);
                if (token == null)
                {
                    return BadRequest("Pogrešni podaci za prijavu.");
                }
                return Ok(token);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Greška prilikom prijave: {e.Message}");
            }
        }
    }
}
