using TicketingApi.Models;

namespace TicketingApi.Services.Interfaces;

public interface IProjectService {
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel?> GetProjectByIdAsync(int id);
    Task CreateProjectAsync(ProjectModel project);
    Task UpdateProjectAsync(ProjectModel project);
    Task DeleteProjectAsync(int id);
}