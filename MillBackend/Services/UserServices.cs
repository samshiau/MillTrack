using MillBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MillBackend.Services{
public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Method to verify if a user exists with the given username and password
    public async Task<bool> IsUserValid(string username, string password)
    {
        return await _context.Users
            .AnyAsync(u => u.Username == username && u.Password == password);
    }
}

}