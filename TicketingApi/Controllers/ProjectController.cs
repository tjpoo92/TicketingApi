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
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProjectByID(int id) {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpGet("{projectID}/tasks")]
    public async Task<ActionResult<IEnumerable<Task>>> GetTasksForProject(int projectID) {
        var tasks = await _projectService.GetTasksByProjectIdAsync(projectID);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectModel>> CreateProject(ProjectModel project) {
        var newProject = await _projectService.CreateProjectAsync(project);
        return CreatedAtAction(nameof(GetProjectByID), new {id=newProject.project_id}, newProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, ProjectModel project) {
        if (id != project.project_id) return BadRequest();

        await _projectService.UpdateProjectAsync(project);
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProject(int id, [FromBody] JsonPatchDocument<ProjectModel> patchDocument) {
        if (patchDocument == null) return BadRequest();

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();

        patchDocument.ApplyTo(project, ModelState);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _projectService.UpdateProjectAsync(project);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) {
        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }
}