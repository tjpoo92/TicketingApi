using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TicketingApi.Models;
using TicketingApi.Services;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : Controller {
    private readonly ProjectService _projectService;

    public ProjectController(ProjectService projectService) {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProjects() {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProjectById(int id) {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public async Task CreateProject([FromBody] ProjectModel project) {
        await _projectService.CreateProjectAsync(project);
        // return CreatedAtAction(nameof(GetProjectByID), new {id=newProject.ProjectId}, newProject);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchProject(int id, [FromBody] ProjectModel project) {
        if (id != project.ProjectId) return BadRequest();
        
        await _projectService.UpdateProjectAsync(project);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id) {
        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }
}