using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;

[ApiController]
[Route("api/[controller]")]

public class UserController : Controller {
    // Add service reference when created

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers() {
        var users = ""; //Service Call
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUserByID(int id) {
        var user = ""; // Service Call
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("{userID}/tasks")]
    public async Task<ActionResult<IEnumerable<Task>>> GetTasksForUser(int userID) {
        var tasks = ""; // Service Call
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<UserModel>> CreateUser(UserModel user) {
        var newUser = ""; //Service call
        return CreatedAtAction(nameof(GetUserByID), new {id=newUser.user_id}, newUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserModel user) {
        if (id != user.user_id) return BadRequest();

        // Service Call
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(int id, [FromBody] JsonPatchDocument<UserModel> patchDocument) {
        if (patchDocument == null) return BadRequest();

        var user = ""; // Service Call
        if (user == null) return NotFound();

        patchDocument.ApplyTo(user, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Service Call
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) {
        //Service Call
        return NoContent();
    }

}