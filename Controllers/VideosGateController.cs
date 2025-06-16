using AHTAPI.Models;
using AHTAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AHTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class VideosGateController : ControllerBase
    {
        VideosGateRepository videosGateRepository;
        public VideosGateController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            videosGateRepository = new VideosGateRepository(connectionString);
        }
        

        [HttpGet("{name}/{leftRight}")]
        public IEnumerable<GateVideos> GetBeltById(string name, string leftRight)
        {
            var data = videosGateRepository.GetBeltById(name, leftRight);
            return data;
        }
        [HttpGet("{name}")]
        public IEnumerable<GateVideos> GetProductById(string Name)
        {
            var data = videosGateRepository.GetBeltByName(Name);
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] GateVideos newProduct)
        {
            if (videosGateRepository.AddVideosToGate(newProduct))
            {
                // Trả về phản hồi 201 Created cùng với sản phẩm đã được tạo
                //return CreatedAtAction(nameof(GetBeltById), new { name = newProduct.Name, leftRight = newProduct.LeftRight }, newProduct);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{name}/{leftRight}")]
        public async Task<IActionResult> DeleteVideosGate(string name, string leftRight)
        {
            if (videosGateRepository.RemoveVideosFromGate(name, leftRight))
            {
                // Trả về phản hồi 201 Created cùng với sản phẩm đã được tạo
                return Ok();
            }
            return NotFound();
        }

        }

    
}
