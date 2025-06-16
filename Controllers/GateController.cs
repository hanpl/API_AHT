using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        GateRepository gateRepository;
        public GateController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            gateRepository = new GateRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_Departures> GetFlightbyGate()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Console.WriteLine(ipAddress);
            var date = GetFormattedDate();
            return gateRepository.FlightListByGate(date, ipAddress);
        }

        private string GetFormattedDate()
        {
            DateTime now = DateTime.Now; // Lấy ngày và giờ hiện tại

            if (now.Hour >= 0 && now.Hour < 5)
            {
                // Nếu giờ từ 00:00 đến 04:59, trả về ngày hôm trước
                now = now.AddDays(-1);
            }
            return now.ToString("yyyy-MM-dd");
        }
    }

    
}
