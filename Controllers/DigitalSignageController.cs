using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalSignageController : ControllerBase
    {
         DigitalSignageRepository digitalSignageRepository;
        public DigitalSignageController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            digitalSignageRepository = new DigitalSignageRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<DigitalSignage> GetAllGateInfor()
        {
            return digitalSignageRepository.GetGateInfor();
        }

        [HttpGet("{Name}/{Gate}")]
        public IEnumerable<DigitalSignage> GetDigitalById(string Name,string Gate)
        {
            var product = digitalSignageRepository.GetDigitalByGateNumber(Name, Gate);
            return product;
        }
    }
}
