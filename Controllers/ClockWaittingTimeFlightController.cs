using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockWaittingTimeFlightController : ControllerBase
    {
        ClockWaittingTimeFlightRepository clockWaittingTimeFlightRepository;
        public ClockWaittingTimeFlightController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection2");
            clockWaittingTimeFlightRepository = new ClockWaittingTimeFlightRepository(connectionString);
        }

        [HttpGet]
        public IEnumerable<ClockWTTFlight> GetFlightbyGate(string name)
        {
            return clockWaittingTimeFlightRepository.GetWaitingTimeFlights(name);
        }

    }
}
