using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingDepController : ControllerBase
    {
        SortingRepository sortingRepository;
        public SortingDepController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            sortingRepository = new SortingRepository(connectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlightSortingDep( int top ,  int timeStart ,  int timeEnd , string location, int numberM )
        {
             var flights = await  sortingRepository.GetFlightSortingDep(top, timeStart, timeEnd,location, numberM );
            Console.WriteLine("chami1" + top + " " + timeStart + " " + timeEnd + "  " + location + "  " + numberM);
            return Ok(flights);
        }

    }
}
