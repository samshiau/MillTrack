using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MillBackend.Services;
using MillBackend.Models;

namespace MillBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // 1. Add a new inventory item to the database
        [HttpPost("add")]
        public async Task<IActionResult> AddInventoryItem([FromBody] InventoryItem item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _inventoryService.AddInventoryItemAsync(item);
            return Ok(new { message = "Inventory item added successfully!" });
        }

        // 2. Get all inventory items from the database
        [HttpGet("all")]
        public async Task<ActionResult<List<InventoryItem>>> GetAllInventoryItems()
        {
            var items = await _inventoryService.GetAllInventoryItemsAsync();
            return Ok(items);
        }
    }
}
