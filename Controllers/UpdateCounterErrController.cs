using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateCounterErrController : ControllerBase
    {
        UpdateCounterErrRepository updateCounterErrRepository;
        public UpdateCounterErrController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            updateCounterErrRepository = new UpdateCounterErrRepository(connectionString);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpdateCounterErr data)
        {
            // Gọi phương thức AddDataToDB một cách bất đồng bộ
            Console.WriteLine(data.Id + " "+ data.CheckinCounters);
            bool result = await updateCounterErrRepository.UpdateDataToDB(data);
            if (result)
            {
                return Ok("Data uploaded successfully.");
            }

            return BadRequest("Invalid data.");
        }
    }
}
