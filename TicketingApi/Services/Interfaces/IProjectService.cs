using TicketingApi.Models;

public interface IProjectService
{
    Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
    Task<ProjectModel> GetProjectByIdAsync(int id);
    Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID);
    Task<ProjectModel> CreateProjectAsync(ProjectModel project);
    Task UpdateProjectAsync(ProjectModel project);
    Task DeleteProjectAsync(int id);
}