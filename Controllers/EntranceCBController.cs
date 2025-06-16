using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntranceCBController : ControllerBase
    {
        EntranceCRepository entranceRepository;
        public EntranceCBController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            entranceRepository = new EntranceCRepository(connectionString);
        }

        [HttpGet]
        public IEnumerable<AHT_EntranceC> GetAllEntrance()
        {
            return entranceRepository.GetEntranceB();
        }
    }
}
