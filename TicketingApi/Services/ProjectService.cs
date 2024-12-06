using TicketingApi.Models;

public class ProjectService : IProjectService {
    private readonly IProjectRepository _projectRepository;
    private readonly Validator _validator;

    public ProjectService(IProjectRepository projectRepository, Validator validator) {
        _projectRepository = projectRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync() {
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<ProjectModel> GetProjectByIdAsync(int id) {
        _validator.ValidateId(id, "Project");

        var project = await _projectRepository.GetProjectByIdAsync(id);
        _validator.ValidateObjectNotNull(project, "Project");

        return project;
    }

    public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
    {
        _validator.ValidateObjectNotNull(project, "Project");

        return await _projectRepository.CreateProjectAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectModel project)
    {
        _validator.ValidateObjectNotNull(project, "Project");

        var existingProject = await _projectRepository.GetProjectByIdAsync(project.ProjectId);
        _validator.ValidateObjectNotNull(existingProject, "Project");
        
        await _projectRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        _validator.ValidateId(id, "Project");

        var existingProject = await _projectRepository.GetProjectByIdAsync(id);
        _validator.ValidateObjectNotNull(existingProject, "Project");
        
        await _projectRepository.DeleteProjectAsync(id);
    }
}