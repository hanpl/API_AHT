using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBLEController : ControllerBase
    {
        EmployeeBLERepository employeeBLERepository;
        public EmployeeBLEController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            employeeBLERepository = new EmployeeBLERepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_EmployeeBLE> GetCountries()
        {
            return employeeBLERepository.CountriesList();
        }
    }
}
