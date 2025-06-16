using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        CountriesRepository countriesRepository;
        public CountriesController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            countriesRepository = new CountriesRepository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_Countries> GetCountries()
        {
            return countriesRepository.CountriesList();
        }
    }
}
