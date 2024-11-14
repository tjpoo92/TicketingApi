using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;

[ApiController]
[Route("api/[controller]")]

public class ProjectController : Controller {
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService) {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProjects() {
        var projects = ""; //Service Call
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProjectByID(int id) {
        var project = ""; // Service Call
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpGet("{projectID}/tasks")]
    public async Task<ActionResult<IEnumerable<Task>>> GetTasksForProject(int projectID) {
        var tasks = ""; // Service Call
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectModel>> CreateProject(ProjectModel project) {
        var newProject = ""; //Service call
        return CreatedAtAction(nameof(GetProjectByID), new {id=newProject.project_id}, newProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectModel project) {
        if (id != project.project_id) return BadRequest();

        // Service Call
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProject(int id, [FromBody] JsonPatchDocument<ProjectModel> patchDocument) {
        if (patchDocument == null) return BadRequest();

        var project = ""; // Service Call
        if (project == null) return NotFound();

        patchDocument.ApplyTo(project, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Service Call
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) {
        //Service Call
        return NoContent();
    }

}