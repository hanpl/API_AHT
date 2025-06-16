using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateUserController : ControllerBase
    {
        RateUserRepository rateUserRepository;
        public RateUserController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection1821");
            rateUserRepository = new RateUserRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AlertStateZalo> GetAlert()
        {
            return rateUserRepository.GetAlert();
        }
    }
}
