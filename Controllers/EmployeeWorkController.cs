using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeWorkController : ControllerBase
    {
        EmployeeWorkRepository eEmployeeWorkRepository;
        public EmployeeWorkController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection1820");
            eEmployeeWorkRepository = new EmployeeWorkRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_EmployeeWorkDaily> GetAllName()
        {
            return eEmployeeWorkRepository.GetAllEmployee();
        }
    }
}
