using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AHTDigitalSignageController : ControllerBase
    {
        DigitalSignageRepository digitalSignageRepository;
        public AHTDigitalSignageController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            digitalSignageRepository = new DigitalSignageRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_DigitalSignage> GetAllDigitalSignalges()
        {
            var data = digitalSignageRepository.GetAllDigitalSignalges();
            return data;
        }

        [HttpGet("{Name}")]
        public IEnumerable<AHT_DigitalSignage> GetDigitalByName(string Name)
        {
            var data = digitalSignageRepository.GetDigitalByName(Name);
            return data;
        }
    }
}
