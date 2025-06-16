using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalSignageController2 : ControllerBase
    {
        DigitalSignageRepository digitalSignageRepository;
        public DigitalSignageController2(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            digitalSignageRepository = new DigitalSignageRepository(connectionString);
        }


        [HttpGet("{Name}")]
        public FlightForComple GetDigitalById(string Name)
        {
            var data = digitalSignageRepository.GetFlightByGateNumber(Name);
            return data;
        }

    }
}
