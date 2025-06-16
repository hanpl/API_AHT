using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageForFlightController : ControllerBase
    {
        ImageForFlightRepository imageForFlightRepository;
        public ImageForFlightController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            imageForFlightRepository = new ImageForFlightRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_ImageForFlight> GetAllName()
        {
            return imageForFlightRepository.GetAllName();
        }

        [HttpGet("{Name}")]
        public AHT_ImageForFlight? GetImgByName(string Name)
        {
            var data = imageForFlightRepository.GetImgByName(Name);
            return data;
        }

        [HttpPost]
        public AHT_ImageForFlight? PostImgByName(string Name)
        {
            if (imageForFlightRepository.PostImgByName(Name))
            {
                Console.WriteLine("Post ok" + Name);
                var data = imageForFlightRepository.GetImgByName(Name);
                return data;
            }
            return null;
        }

    }
}
