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
            var data = digitalSignageRepository.GetGateInfor();
            return data;
        }

        [HttpGet("{Name}/{LeftRight}")]
        public IEnumerable<DigitalSignage> GetDigitalById(string Name,string LeftRight)
        {
            var data = digitalSignageRepository.GetDigitalByGateNumber(Name, LeftRight);
                return data;
        }

        [HttpPut("{Name}/{LeftRight}")]
        public IActionResult Put(string Name, string LeftRight, string columnName, string des)
        {

            if (digitalSignageRepository.UpdateDigitalByColumnName(Name, LeftRight, columnName, des))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
