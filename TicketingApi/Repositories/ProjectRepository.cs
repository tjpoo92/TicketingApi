using TicketingApi.Models;

public class ProjectRepository : IProjectRepository
{
    public Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectModel> GetProjectByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        throw new NotImplementedException();
    }
    public Task<ProjectModel> CreateProjectAsync(ProjectModel project)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProjectAsync(ProjectModel project)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }
}