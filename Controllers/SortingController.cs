using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        SortingRepository sortingRepository;
        public SortingController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            sortingRepository = new SortingRepository(connectionString);
        }

        [HttpGet]
        public ActionResult<AHT_SortingInfor> GetFlightbyC()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine("ipAddress:  " + ipAddress);

            var device = sortingRepository.GetDeviceByIp(ipAddress);
            Console.WriteLine(device.RollOff);
            if (device == null)
                return NotFound("Không tìm thấy thiết bị theo IP");
            return Ok(device);
        }
    }
}
