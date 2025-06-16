using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateModeCounterController : ControllerBase
    {
        UpdateModeCounterRepository updateModeCounterRepository;
        public UpdateModeCounterController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            updateModeCounterRepository = new UpdateModeCounterRepository(connectionString);
        }


        // POST: api/Mode/UpdateMode
        [HttpPost("UpdateMode")]
        public async Task<IActionResult> UpdateMode([FromBody] AHT_CounterCheckin request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Mode))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateMode(request.Name, request.Mode);

        }

        // POST: api/Mode/UpdateMode
        [HttpPost("UpdateAuto")]
        public async Task<IActionResult> UpdateAuto([FromBody] AHT_CounterCheckin request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Auto))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateAuto(request.Name, request.Auto);

        }


        

        // POST: api/Mode/UpdateMode
        [HttpPost("UpdateNomal")]
        public async Task<IActionResult> UpdateNomal([FromBody] AHT_ImageForFlight request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Nomal))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateNomal(request.Name, request.Nomal);

        }

        [HttpPost("UpdateEco")]
        public async Task<IActionResult> UpdateEco([FromBody] AHT_ImageForFlight request)
        {
            Console.WriteLine(request.Name + " " + request.Eco);
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Eco))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateEco(request.Name, request.Eco);

        }

        [HttpPost("UpdateBus")]
        public async Task<IActionResult> UpdateBus([FromBody] AHT_ImageForFlight request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Bus))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateBus(request.Name, request.Bus);

        }

        [HttpPost("UpdateManual")]
        public async Task<IActionResult> UpdateManual([FromBody] AHT_ImageForFlight request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Manual))
            {
                return BadRequest("Invalid request data.");
            }
            return updateModeCounterRepository.UpdateManual(request.Name, request.Manual);

        }




    }
}
