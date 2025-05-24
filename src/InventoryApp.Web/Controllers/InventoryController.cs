using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Application.DTOs;
using InventoryApp.Application.Services;
using InventoryApp.Web.ViewModels;

namespace InventoryApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
            => _inventoryService = inventoryService;

        [HttpPost("adjust")]
        public async Task<IActionResult> Adjust([FromBody] InventoryAdjustVm vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new InventoryAdjustDto { ProductId = vm.ProductId, Delta = vm.Delta, Location = vm.Location };
            await _inventoryService.AdjustAsync(dto);
            return Ok();
        }
    }
}
