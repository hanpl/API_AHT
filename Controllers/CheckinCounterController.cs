using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinCounterController : ControllerBase
    {
        CheckinCounterRepository checkinCounterRepository;
        public CheckinCounterController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            checkinCounterRepository = new CheckinCounterRepository(connectionString);
        }

        [HttpGet("{date}/{local}")]
        public IEnumerable<AHT_Counter> GetCounters(string date, string local)
        {
            

            // In ra địa chỉ IP để kiểm tra
            //Console.WriteLine($"Request received from IP: {ipAddress}");

            return checkinCounterRepository.CountersList(date, local);
        }

        [HttpGet("{date}")]
        public IEnumerable<AHT_Departures> GetFlightList(string date)
        {
            return checkinCounterRepository.FlightList(date);
        }

        [HttpGet()]
        public IEnumerable<AHT_Departures> GetFlightbyC()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var date = GetFormattedDate();
            //Console.WriteLine(date +"  "+ ipAddress);
            return checkinCounterRepository.FlightListByC(date, ipAddress);
        }

        private string GetFormattedDate()
        {
            DateTime now = DateTime.Now; // Lấy ngày và giờ hiện tại

            if (now.Hour >= 0 && now.Hour < 5)
            {
                // Nếu giờ từ 00:00 đến 04:59, trả về ngày hôm trước
                now = now.AddDays(-1);
            }

            // Trả về ngày theo định dạng "yyyy-MM-dd"
            return now.ToString("yyyy-MM-dd");
        }

        [HttpPut]
        public IActionResult Put([FromBody] AHT_Departures newflight)
        {
            if (checkinCounterRepository.UpdateFlight(newflight))
            {
                if(checkinCounterRepository.UpdateFlightToGateInformation(newflight))
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
