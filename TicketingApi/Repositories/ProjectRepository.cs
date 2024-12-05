using TicketingApi;
using TicketingApi.Models;

public class ProjectRepository : IProjectRepository
{
    private readonly string _connectionString;
    private SqlDataAccess db = new SqlDataAccess();

    public ProjectRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        string sql = "SELECT * FROM dbo.projects";
        return await db.LoadDataAsync<ProjectModel, dynamic>(sql, new { }, _connectionString);
    }

    public async Task<ProjectModel> GetProjectByIdAsync(int id)
    {
        string sql = "SELECT * FROM dbo.projects WHERE project_id = @Id";
        List<ProjectModel> output = await db.LoadDataAsync<ProjectModel, dynamic>(sql, new { Id = id}, _connectionString);
        return output.First();
    }

    public async Task CreateProjectAsync(ProjectModel project)
    {
        string sql = "INSERT INTO dbo.projects " +
            "(created_by, date_due, date_completed, project_name, project_description, status, priority) VALUES " +
            "(@CreatedBy, @DateDue, @DateCompleted, @ProjectName, @ProjectDescription, @Status, @Priority)";
        await db.SaveDataAsync(sql,
            new { project.CreatedBy, project.DateDue, project.DateCompleted, project.ProjectName, project.ProjectDescription, project.Status, project.Priority },
            _connectionString);
        return;
    }

    public async Task UpdateProjectAsync(ProjectModel project)
    {
        ProjectModel oldProject = await GetProjectByIdAsync(project.ProjectId);
        if (oldProject == null)
        {
            await CreateProjectAsync(project);
            return;
        }
        else
        {
            ProjectModel updateProject = new ProjectModel
            {
                ProjectId = project.ProjectId,
                CreatedBy = project.CreatedBy == null ? oldProject.CreatedBy : project.CreatedBy,
                DateDue = project.DateDue == null ? oldProject.DateDue : project.DateDue,
                ProjectName = project.ProjectName ?? oldProject.ProjectName,
                ProjectDescription = project.ProjectDescription ?? oldProject.ProjectDescription,
                Status = project.Status == null ? oldProject.Status : project.Status,
                Priority = project.Priority == null ? oldProject.Priority : project.Priority
            };
            string sql = "UPDATE dbo.projects SET created_by = @CreatedBy, " +
                "date_due = @DateDue, date_completed = @DateCompleted, project_name = @ProjectName, " +
                "project_description = @ProjectDescription, status = @Status, priority = @Priority WHERE project_id = @ProjectId";
            await db.SaveDataAsync(sql, updateProject, _connectionString);
            return;
        }
    }

    public async Task DeleteProjectAsync(int id)
    {
        string sql = "DELETE FROM dbo.projects WHERE project_id = @Id";
        await db.SaveDataAsync(sql, new { Id= id }, _connectionString);
    }
}