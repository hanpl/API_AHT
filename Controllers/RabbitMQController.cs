using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMQController : ControllerBase
    {
        RabbitMQRepository rabbitMQRepository;
        public RabbitMQController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection1");
            rabbitMQRepository = new RabbitMQRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_T1_FlightInformation> GetAllRabbitMQ()
        {
            return rabbitMQRepository.GetAllRabbitMQ();
        }

        [HttpGet("{id}")]
        public ActionResult<AHT_T1_FlightInformation> GetProductById(string id)
        {
            var product = rabbitMQRepository.GetFlightInformation(id);
            if (product == null)
            {
                return NotFound();
            }
                return product;  
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<RabbitMQ> newflight)
        {
            if (rabbitMQRepository.AddRabbitMQ(newflight))
            {
                return Ok(newflight); 
            }
            return BadRequest();
        }

        
    }
}
