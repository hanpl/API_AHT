using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidsArrivalsController : ControllerBase
    {
        ArrivalsRepository arrivalsRepository;
        public FidsArrivalsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            arrivalsRepository = new ArrivalsRepository(connectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlightFidsArr(int rollOn, int rollOff)
        {
            var flights = await arrivalsRepository.GetFlightFidsArr(rollOn, rollOff);
            return Ok(flights);
        }
    }
}
