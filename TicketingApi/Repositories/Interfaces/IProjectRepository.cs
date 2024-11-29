using TicketingApi.Models;

public interface IProjectRepository {
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel> GetProjectByIdAsync(int id);
    Task<ProjectModel> CreateProjectAsync(ProjectModel project);
    Task UpdateProjectAsync(ProjectModel project);
    Task DeleteProjectAsync(int id);
}