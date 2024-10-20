using Microsoft.AspNetCore.Mvc;
using MillBackend.Services; // Replace with your actual namespace

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // POST: api/user/validate
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateUser([FromBody] UserLoginRequest request)
    {
        bool isValidUser = await _userService.IsUserValid(request.Username, request.Password);

        if (!isValidUser)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(new { message = "User authenticated successfully" });
    }
}

// Model for the request
public class UserLoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
