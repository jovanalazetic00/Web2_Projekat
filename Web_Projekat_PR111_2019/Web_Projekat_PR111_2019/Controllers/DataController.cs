using Microsoft.AspNetCore.Mvc;

namespace Web_Projekat_PR111_2019.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Implementirajte kod za dohvatanje podataka iz baze ili drugog izvora

            // Primer: Vraćanje fiksiranih podataka
            var data = new { message = "Hello from backend!" };

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] dynamic requestData)
        {
            // Implementirajte kod za obradu podataka i upisivanje u bazu ili drugu akciju

            // Primer: Vraćanje primljenih podataka
            return Ok(requestData);
        }
    }
}
