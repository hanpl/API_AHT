using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalsController : ControllerBase
    {
        ArrivalsRepository arrivalsRepository;
        public ArrivalsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            arrivalsRepository = new ArrivalsRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_Arrivals> GetListFlight()
        {
            return arrivalsRepository.ArrivalsList();
        }
    }
}
