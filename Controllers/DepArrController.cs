using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepArrController : ControllerBase
    {
        DepArrRepository depArrRepository;
        public DepArrController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            depArrRepository = new DepArrRepository(connectionString);
        }

        [HttpGet("{Date}/{Adi}")]
        public IEnumerable<AHT_DepArr> GetDepArrList(string Date, string Adi)
        {
            return depArrRepository.DepArrList(Date, Adi);
        }
    }
}
