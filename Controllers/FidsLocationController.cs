using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidsLocationController : ControllerBase
    {
        FidsLocationRepository fidsSpareRepository;
        public FidsLocationController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            fidsSpareRepository = new FidsLocationRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_FidsInformation> GetLocation() 
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            //return fidsSpareRepository.GetLocationByIp(ipAddress);
            Console.WriteLine(ipAddress);
            return fidsSpareRepository.GetLocationByIp(ipAddress);
        }

    }
}
