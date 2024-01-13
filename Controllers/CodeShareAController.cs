using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeShareAController : ControllerBase
    {
        EntranceRepository entranceRepository;
        public CodeShareAController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            entranceRepository = new EntranceRepository(connectionString);
        }

        [HttpGet]
        public IEnumerable<CodeShare> GetAllCodeShare()
        {
            return entranceRepository.GetCodeShareA();
        }
    }
}
