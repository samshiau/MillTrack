using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MillBackend.Services;
using MillBackend.Models;
using System.Net.Http;

namespace MillBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        private readonly HttpClient _httpClient;

        public InventoryController(InventoryService inventoryService, HttpClient httpClient)
        {
            _inventoryService = inventoryService;
            _httpClient = httpClient;
        }

        // 1. Add a new inventory item to the database
        [HttpPost("add")]
        public async Task<IActionResult> AddInventoryItem([FromBody] InventoryItem item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _inventoryService.AddInventoryItemAsync(item);

            string title="New item in inventory";
            string body="A new item has been added to the inventory. Please check the inventory to see the new item.";
            string toEmail="shiausamuel@gmail.com";

            string url=$"http://localhost:7071/api/SendEmailFunction?toEmail={toEmail}&subject={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";

            await _httpClient.PostAsync(url, null);

            return Ok(new { message = "Inventory item added successfully!" });
        }

        // 2. Get all inventory items from the database
        [HttpGet("all")]
        public async Task<ActionResult<List<InventoryItem>>> GetAllInventoryItems()
        {
            var items = await _inventoryService.GetAllInventoryItemsAsync();
            return Ok(items);
        }

        // 3. Update an existing inventory item by ID
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] InventoryItem item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldItem = await _inventoryService.GetItemByIdAsync(id);
            var updatedItem = await _inventoryService.UpdateInventoryItemAsync(id, item);

            string title="Inventory item updated";
            string body="A new item has been updated. Please check the inventory.";
            string toEmail="shiausamuel@gmail.com";

            string url=$"http://localhost:7071/api/SendEmailFunction?toEmail={toEmail}&subject={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";

            await _httpClient.PostAsync(url, null);
           
            if (item.AmountInStock <= 0){

                title="Item is out of stock";
                body="An item in the inventory is out of stock. Please check the inventory.";
                url = $"http://localhost:7071/api/SendEmailFunction?toEmail={toEmail}&subject={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";
                await _httpClient.PostAsync(url, null);
            }
           

            if (updatedItem == null)
                return NotFound(new { message = "Item not found." });

            return Ok(new { message = "Inventory item updated successfully!" });
        }

        // 4. Delete an inventory item by ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var isDeleted = await _inventoryService.DeleteInventoryItemAsync(id);



            if (!isDeleted)
                return NotFound(new { message = "Item not found." });

            string title="Item deleted from inventory";
            string body="An item in the inventory has been removed.";
            string toEmail="shiausamuel@gmail.com";
            string url = $"http://localhost:7071/api/SendEmailFunction?toEmail={toEmail}&subject={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";
            
            await _httpClient.PostAsync(url, null);

            return Ok(new { message = "Inventory item deleted successfully!" });
        }
    }
}
