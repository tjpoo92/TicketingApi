using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;

[ApiController]
[Route("api/[controller]")]

public class TaskController : Controller {
    // Add service reference when created

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks() {
        var tasks = ""; //Service Call
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTaskByID(int id) {
        var task = ""; // Service Call
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task) {
        var newTask = ""; //Service call
        return CreatedAtAction(nameof(GetTaskByID), new {id=newTask.task_id}, newTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskModel task) {
        if (id != task.task_id) return BadRequest();

        // Service Call
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchTask(int id, [FromBody] JsonPatchDocument<TaskModel> patchDocument) {
        if (patchDocument == null) return BadRequest();

        var task = ""; // Service Call
        if (task == null) return NotFound();

        patchDocument.ApplyTo(task, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Service Call
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id) {
        //Service Call
        return NoContent();
    }

}