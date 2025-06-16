using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidsSpareController : ControllerBase
    {
        FidsSpareRepository fidsSpareRepository;
        public FidsSpareController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            fidsSpareRepository = new FidsSpareRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_Departures> GetListFlight()
        {
            return fidsSpareRepository.DeparturesList();
        }
    }

    
}
