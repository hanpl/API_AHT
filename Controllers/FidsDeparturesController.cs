using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidsDeparturesController : ControllerBase
    {
        FidsSpareRepository fidsSpareRepository;
        public FidsDeparturesController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            fidsSpareRepository = new FidsSpareRepository(connectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlightFidsDep(int rollOn, int rollOff)
        {
            var flights = await fidsSpareRepository.GetFlightFidsDep(rollOn, rollOff);
            return Ok(flights);
        }


    }
}
