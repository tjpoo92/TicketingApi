using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;
using TicketingApi.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class UserController : Controller {
    private readonly IUserService _userService;

    public UserController (IUserService userService) {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers() {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUserById(int id) {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task CreateUser([FromBody] UserModel user) {
        await _userService.CreateUserAsync(user);
        // return CreatedAtAction(nameof(GetUserByID), new {id=newUser.UserId}, newUser);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(int id, [FromBody] UserModel user) {
        if (id != user.UserId) return BadRequest();
    
        await _userService.UpdateUserAsync(user);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }

}