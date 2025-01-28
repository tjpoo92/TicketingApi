using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;

[ApiController]
[Route("api/[controller]")]

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

    // [HttpGet("{id}")]
    // public async Task<ActionResult<UserModel>> GetUserByID(int id) {
    //     var user = await _userService.GetUserByIdAsync(id);
    //     if (user == null) return NotFound();
    //     return Ok(user);
    // }

    [HttpPost]
    public async Task CreateUser(UserModel user) {
        await _userService.CreateUserAsync(user);
        // return CreatedAtAction(nameof(GetUserByID), new {id=newUser.UserId}, newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserModel user) {
        if (id != user.UserId) return BadRequest();

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    // [HttpPatch("{id}")]
    // public async Task<IActionResult> PatchUser(int id, [FromBody] JsonPatchDocument<UserModel> patchDocument) {
    //     if (patchDocument == null) return BadRequest();
    //
    //     var user = await _userService.GetUserByIdAsync(id);
    //     if (user == null) return NotFound();
    //
    //     patchDocument.ApplyTo(user, ModelState);
    //
    //     if (!ModelState.IsValid) return BadRequest(ModelState);
    //
    //     await _userService.UpdateUserAsync(user);
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteUser(int id) {
    //     await _userService.DeleteUserAsync(id);
    //     return NoContent();
    // }

}