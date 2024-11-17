using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;

[ApiController]
[Route("api/[controller]")]

public class TaskController : Controller {
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService) {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllTasks() {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskModel>> GetTaskByID(int id) {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task) {
        var newTask = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskByID), new {id=newTask.task_id}, newTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskModel task) {
        if (id != task.task_id) return BadRequest();

        await _taskService.UpdateTaskAsync(task);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchTask(int id, [FromBody] JsonPatchDocument<TaskModel> patchDocument) {
        if (patchDocument == null) return BadRequest();

        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null) return NotFound();

        patchDocument.ApplyTo(task, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _taskService.UpdateTaskAsync(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id) {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }

}