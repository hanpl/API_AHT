using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandsController : ControllerBase
    {
        StandsRepository standsRepository;
        public StandsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            standsRepository = new StandsRepository(connectionString);
        }

        [HttpPost]
        public IActionResult PostStandByName(string name, string devices)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name must not be empty.");
            }

            bool result = standsRepository.PostStandByName(name, devices);

            if (result)
            {
                Console.WriteLine("Post ok: " + name);
                return Ok(new { message = $"Image for '{name}' posted successfully." });
            }
            else
            {
                return NotFound(new { message = $"Image post failed. No record found with name '{name}'." });
            }
        }

    }
}
