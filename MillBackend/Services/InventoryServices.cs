using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MillBackend.Models;

namespace MillBackend.Services
{
    public class InventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Add a new inventory item to the database
        public async Task AddInventoryItemAsync(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
        }

        // 2. Retrieve all inventory items from the database
        public async Task<List<InventoryItem>> GetAllInventoryItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItem> UpdateInventoryItemAsync(int id, InventoryItem updatedItem)
        {
            var existingItem = await _context.InventoryItems.FindAsync(id);

            if (existingItem == null)
            {
                return null;
            }

            // Update the existing item's fields with the new values
            existingItem.Name = updatedItem.Name;
            existingItem.SKU = updatedItem.SKU;
            existingItem.Description = updatedItem.Description;
            existingItem.Category = updatedItem.Category;
            existingItem.Location = updatedItem.Location;
            existingItem.UnitPrice = updatedItem.UnitPrice;
            existingItem.AmountInStock = updatedItem.AmountInStock;
            existingItem.IsActive = updatedItem.IsActive;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        // 4. Delete an inventory item by ID
        public async Task<bool> DeleteInventoryItemAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);

            if (item == null)
            {
                return false;
            }

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            var item= await _context.InventoryItems.FindAsync(id);
            
            if(item == null)
            {
                return null;
            }
            
            return item; 
        }
    }
}
