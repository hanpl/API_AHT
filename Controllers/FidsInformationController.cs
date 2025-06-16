using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FidsInformationController : ControllerBase
    {
        FidsInformationRepository fidsInformationRepository;
        public FidsInformationController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            fidsInformationRepository = new FidsInformationRepository(connectionString);
        }


        [HttpGet]
        public ActionResult<AHT_FidsInformation> GetFlightbyC()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine("ipAddress:  " + ipAddress);
            var device = fidsInformationRepository.GetDeviceByIp(ipAddress);
            if (device == null)
                return NotFound("Không tìm thấy thiết bị theo IP");
            return Ok(device);
        }
    }
}
