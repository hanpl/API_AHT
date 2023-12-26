using Microsoft.AspNetCore.Mvc;
using AHTAPI.Models;
using AHTAPI.Repositories;

namespace WOServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        WorkOrderRepository workOrderRepository;
        public WorkOrderController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            workOrderRepository = new WorkOrderRepository(connectionString);
        }
        [HttpGet]
        public IEnumerable<AHT_WorkOrder> GetAllWorkOrder()
        {
            return workOrderRepository.GetWorkOrder();
        }


        [HttpGet("{id}")]
        public ActionResult<AHT_WorkOrder> GetProductById(int id)
        {
            var product = workOrderRepository.GetWorkOrderById(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] AHT_WorkOrder aHT_WorkOrderUpdate)
        public IActionResult Put(int id, string status, string des)
        {
            var aHT_WorkOrder = workOrderRepository.GetWorkOrderById(id);
            if (aHT_WorkOrder == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin sản phẩm
            aHT_WorkOrder.Status = status;
            aHT_WorkOrder.Description = des;

            if (workOrderRepository.UpdateWorkOrder(aHT_WorkOrder))
            {
                return Ok(aHT_WorkOrder);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] AHT_WorkOrder newProduct)
        {
            if (workOrderRepository.AddWorkOrder(newProduct))
            {
                // Trả về phản hồi 201 Created cùng với sản phẩm đã được tạo
                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
            }
            return NotFound();

            

        }
    }
}
