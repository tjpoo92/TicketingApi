using TicketingApi.Models;

public class ProjectService : IProjectService {
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository) {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync() {
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<ProjectModel> GetProjectByIdAsync(int id) {
        var project = await _projectRepository.GetProjectByIdAsync(id);
        if (project == null) {
            throw new KeyNotFoundException("Project not found.");
        }
        return project;
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        var tasks = await _projectRepository.GetTasksByProjectIdAsync(projectID);
        if (tasks == null) {
            throw new KeyNotFoundException("Tasks not found.");
        }
        return tasks;
    }

    public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
    {
        return await _projectRepository.CreateProjectAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectModel project)
    {
        var existingProject = await _projectRepository.GetProjectByIdAsync(project.project_id);
        if (existingProject == null) {
            throw new KeyNotFoundException("Project not found");
        }
        await _projectRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        var existingProject = await _projectRepository.GetProjectByIdAsync(id);
        if (existingProject == null) {
            throw new KeyNotFoundException("Project not found");
        }
        await _projectRepository.DeleteProjectAsync(id);
    }
}