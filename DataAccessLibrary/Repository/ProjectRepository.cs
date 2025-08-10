using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Repository
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly string _connectionString;
		private SqlDataAccess db = new SqlDataAccess();

		public ProjectRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");
		}

		public Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
		{
			string sql = "SELECT project_id as ProjectId, created_by as CreatedBy, date_due as DateDue, date_completed as DateCompleted, project_name as ProjectName, project_description as ProjectDescription, status as Status, priority as Priority, created_at as CreatedAt, updated_at as UpdatedAt FROM dbo.projects";
			return db.LoadDataAsync<ProjectEntity, dynamic>(sql, new { }, _connectionString);
		}
		
		public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
		{
			string sql = "SELECT project_id as ProjectId, created_by as CreatedBy, date_due as DateDue, date_completed as DateCompleted, project_name as ProjectName, project_description as ProjectDescription, status as Status, priority as Priority, created_at as CreatedAt, updated_at as UpdatedAt FROM dbo.projects WHERE project_id = @Id";
			return await db.LoadSingleDataAsync<ProjectEntity, dynamic>(sql, new { Id = id }, _connectionString);
		}

		public async Task CreateProjectAsync(ProjectEntity project)
		{
			project.CreatedAt = DateTime.Now;
			project.UpdatedAt = DateTime.Now;
			
			string sql = "INSERT INTO dbo.projects " +
						 "(created_by, date_due, date_completed, project_name, project_description, status, priority, created_at, updated_at) VALUES " +
						 "(@CreatedBy, @DateDue, @DateCompleted, @ProjectName, @ProjectDescription, @Status, @Priority, @CreatedAt, @UpdatedAt)";
			await db.SaveDataAsync(sql,
				project,
				_connectionString);
		}
		
		public async Task UpdateProjectAsync(ProjectEntity project)
		{
			ProjectEntity oldProject = await GetProjectByIdAsync(project.ProjectId);
			if (oldProject == null)
			{
				await CreateProjectAsync(project);
			}
			else
			{
				ProjectEntity updateProject = new()
				{
					ProjectId = project.ProjectId,
					CreatedBy = project.CreatedBy == null ? oldProject.CreatedBy : project.CreatedBy,
					DateDue = project.DateDue ?? oldProject.DateDue,
					DateCompleted = project.DateCompleted ?? oldProject.DateCompleted,
					ProjectName = project.ProjectName ?? oldProject.ProjectName,
					ProjectDescription = project.ProjectDescription ?? oldProject.ProjectDescription,
					Status = project.Status == null ? oldProject.Status : project.Status,
					Priority = project.Priority == null ? oldProject.Priority : project.Priority,
					UpdatedAt = DateTime.Now
				};
				string sql = "UPDATE dbo.projects SET created_by = @CreatedBy, " +
					"date_due = @DateDue, date_completed = @DateCompleted, project_name = @ProjectName, " +
					"project_description = @ProjectDescription, status = @Status, priority = @Priority, updated_at = @UpdatedAt WHERE project_id = @ProjectId";
				await db.SaveDataAsync(sql, updateProject, _connectionString);
			}
		}
		
		public async Task DeleteProjectAsync(int id)
		{
			string sql = "DELETE FROM dbo.projects WHERE project_id = @Id";
			await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
		}
	}
}