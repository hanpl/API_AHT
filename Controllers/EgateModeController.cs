using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EgateModeController : ControllerBase
    {
        EgateModeRepository egateModeRepository;
        public EgateModeController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            egateModeRepository = new EgateModeRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_EGATE> GetAllName()
        {
            return egateModeRepository.GetAllName();
        }

        //[HttpGet("{id}")]
        //public IEnumerable<AHT_EGateForFlight> GetEgateById(int id)
        //{
        //    var product = egateModeRepository.GetEgateById(id);

        //    return product;
        //}

        [HttpGet("{Name}")]
        public IEnumerable<AHT_EGateForFlight> GetEGateByName(string Name)
        {
            var data = egateModeRepository.GetEGateByName(Name);
            return data;
        }



        [HttpPost]
        //public IActionResult Post([FromBody] AHT_EGateForFlight newProduct)
        //{
        //    if (egateModeRepository.AddEgate(newProduct))
        //    {
        //        // Trả về phản hồi 201 Created cùng với sản phẩm đã được tạo
        //        return CreatedAtAction(nameof(GetEGateByName), new { Name = newProduct.Name }, newProduct);
        //        //return Ok();
        //    }
        //    return NotFound();
        //}
        public IEnumerable<AHT_EGateForFlight> PostEGateByName(string Name)
        {
            if(egateModeRepository.PostEGateByName(Name))
            {
                Console.WriteLine("Post ok" + Name);
                var data = egateModeRepository.GetEGateByName(Name);
                return data;
            }    
            return null;
        }

        [HttpPut]
        //public IActionResult Put(int id, [FromBody] AHT_WorkOrder aHT_WorkOrderUpdate)
        public IEnumerable<AHT_EGateForFlight> PutEgateById([FromBody] AHT_EGateForFlight newEgate)
        {
            if (egateModeRepository.PutEgateById(newEgate.Id, newEgate.IsEgate))
            {
                var data = egateModeRepository.GetEGateByName(newEgate.Name);
                return data;
            }
            return null;
        }


    }
}
