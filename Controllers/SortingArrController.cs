using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingArrController : ControllerBase
    {
        SortingRepository sortingRepository;
        public SortingArrController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            sortingRepository = new SortingRepository(connectionString);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlightSortingArr( string belt ,  int timeStart ,  int timeEnd)
        {
             var flights = await  sortingRepository.GetFlightSortingArr(belt, timeStart, timeEnd );
             return Ok(flights);
        }

    }
}
