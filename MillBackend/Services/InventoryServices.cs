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
    }
}
