using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeltController : ControllerBase
    {
        BeltRepository beltRepository;
        public BeltController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            beltRepository = new BeltRepository(connectionString);
        }

        [HttpGet("{Name}/{LeftRight}")]
        public IEnumerable<AHT_DigitalSignage> GetBeltById(string Name, string LeftRight)
        {
            var data = beltRepository.GetBeltById(Name, LeftRight);
            return data;
        }
        [HttpGet("{Name}")]
        public IEnumerable<AHT_DigitalSignage> GetBeltByName(string Name)
        {
            var data = beltRepository.GetBeltByName(Name);
            return data;
        }
    }
}
