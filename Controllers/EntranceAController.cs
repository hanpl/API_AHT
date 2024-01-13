using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntranceAController : ControllerBase
    {
        EntranceRepository entranceRepository;
        public EntranceAController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            entranceRepository = new EntranceRepository(connectionString);
        }

        [HttpGet]
        public IEnumerable<AHT_Entrance> GetAllEntrance()
        {
            return entranceRepository.GetEntrance();
        }
    }
}
