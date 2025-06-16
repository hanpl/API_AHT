using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class T1DeparturesController : ControllerBase
    {
        FidsDepartureT1Repository fidsDepartureT1Repository;
        public T1DeparturesController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection1");
            fidsDepartureT1Repository = new FidsDepartureT1Repository(connectionString);
        }

        [HttpGet()]
        public IEnumerable<AHT_T1_Departures> GetListFlight()
        {
            return fidsDepartureT1Repository.DeparturesList();
        }
    }
}
