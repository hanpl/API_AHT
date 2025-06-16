using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitingTimeController : ControllerBase
    {
        WaitingTimeNewRepository waitingTimenewRepository;
        public WaitingTimeController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection2");
            waitingTimenewRepository = new WaitingTimeNewRepository(connectionString);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WaitingTimeFlight data)
        {
            // Gọi phương thức AddDataToDB một cách bất đồng bộ
            bool result = await waitingTimenewRepository.AddDataToDB(data);

            if (result)
            {
                return Ok("Data uploaded successfully.");
            }

            return BadRequest("Invalid data.");
        }
    }
}
