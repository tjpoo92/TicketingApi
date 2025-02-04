using Dapper;
using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Repository
{
	public class TaskRepository : ITaskRepository
	{
		private readonly string _connectionString;
		private SqlDataAccess db = new SqlDataAccess();

		public TaskRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("Default");;
		}

		public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
		{
			string sql = "SELECT task_id AS taskId, project_id AS projectId, created_by AS createdBy, assigned_to AS assignedTo, predessor_task AS predessorTask, date_due AS dateDue, date_completed AS dateCompleted, task_name AS taskName, task_description AS taskDescription, status, priority, created_at AS createdAt, updated_at AS updatedAt FROM dbo.tasks";
			return await db.LoadDataAsync<TaskEntity, dynamic>(sql, new { }, _connectionString);
		}

		public async Task<TaskEntity> GetTaskByIdAsync(int id)
		{
			string sql = "SELECT task_id AS taskId, project_id AS projectId, created_by AS createdBy, assigned_to AS assignedTo, predessor_task AS predessorTask, date_due AS dateDue, date_completed AS dateCompleted, task_name AS taskName, task_description AS taskDescription, status, priority, created_at AS createdAt, updated_at AS updatedAt FROM dbo.tasks WHERE task_id = @Id";
			var tasks = await db.LoadDataAsync<TaskEntity, dynamic>(sql, new { Id = id }, _connectionString);
			return tasks.SingleOrDefault();
		}

		public async Task<IEnumerable<TaskEntity>> GetTasksByProjectIdAsync(int projectID)
		{
			string sql = "SELECT task_id AS taskId, project_id AS projectId, created_by AS createdBy, assigned_to AS assignedTo, predessor_task AS predessorTask, date_due AS dateDue, date_completed AS dateCompleted, task_name AS taskName, task_description AS taskDescription, status, priority, created_at AS createdAt, updated_at AS updatedAt FROM dbo.tasks WHERE project_id = @Id";
			return await db.LoadDataAsync<TaskEntity, dynamic>(sql, new { Id = projectID }, _connectionString);
		}

		public async Task<IEnumerable<TaskEntity>> GetTasksByAssignedUserIdAsync(int userID)
		{
			string sql = "SELECT task_id AS taskId, project_id AS projectId, created_by AS createdBy, assigned_to AS assignedTo, predessor_task AS predessorTask, date_due AS dateDue, date_completed AS dateCompleted, task_name AS taskName, task_description AS taskDescription, status, priority, created_at AS createdAt, updated_at AS updatedAt FROM dbo.tasks WHERE assigned_to = @Id";
			return await db.LoadDataAsync<TaskEntity, dynamic>(sql, new { Id = userID }, _connectionString);
		}

		public async Task CreateTaskAsync(TaskEntity task)
		{
			task.CreatedAt = DateTime.Now;
			task.UpdatedAt = DateTime.Now;
			
			string sql = "INSERT INTO dbo.tasks " +
				"(project_id, created_by, assigned_to, predessor_task, date_due, date_completed, task_name, task_description, status, priority, created_at, updated_at) VALUES " +
				"(@ProjectId, @CreatedBy, @AssignedTo, @PredessorTask, @DateDue, @DateCompleted, @TaskName, @TaskDescription, @Status, @Priority, @CreatedAt, @UpdatedAt)";
			
			await db.SaveDataAsync(sql,
				task,
				_connectionString);
		}

		public async Task UpdateTaskAsync(TaskEntity task)
		{
			TaskEntity oldTask = await GetTaskByIdAsync(task.TaskId);
			if (oldTask == null)
			{
				await CreateTaskAsync(task);
			}
			else
			{
				TaskEntity updateTask = new()
				{
					TaskId = task.TaskId,
					ProjectId = task.ProjectId == null ? oldTask.ProjectId : task.ProjectId,
					CreatedBy = task.CreatedBy == null ? oldTask.CreatedBy : task.CreatedBy,
					AssignedTo = task.AssignedTo == null ? oldTask.AssignedTo : task.AssignedTo,
					PredessorTask = task.PredessorTask == null ? oldTask.PredessorTask : task.PredessorTask,
					DateDue = task.DateDue == null ? oldTask.DateDue : task.DateDue,
					DateCompleted = task.DateCompleted == null ? oldTask.DateCompleted : task.DateCompleted,
					TaskName = task.TaskName ?? oldTask.TaskName,
					TaskDescription = task.TaskDescription ?? oldTask.TaskDescription,
					Status = task.Status == null ? oldTask.Status : task.Status,
					Priority = task.Priority == null ? oldTask.Priority : task.Priority
				};
				string sql = "UPDATE dbo.tasks SET project_id = @ProjectId, " +
					"created_by = @CreatedBy, assigned_to = @AssignedTo, predessor_task = @PredessorTask, " +
					"date_due = @DateDue, date_completed = @DateCompleted, task_name = @TaskName, " +
					"task_description = @TaskDescription, status = @Status, priority = @Priority WHERE task_id = @TaskId";
				await db.SaveDataAsync(sql, updateTask, _connectionString);
			}
		}

		public async Task DeleteTaskAsync(int id)
		{
			string sql = "DELETE FROM dbo.tasks WHERE task_id = @Id";
			await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
		}
	}
}