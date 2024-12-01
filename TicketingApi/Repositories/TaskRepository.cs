using TicketingApi;
using TicketingApi.Models;

public class TaskRepository : ITaskRepository
{
    private readonly string _connectionString;
    private SqlDataAccess db = new SqlDataAccess();

    public TaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        string sql = "SELECT * FROM dbo.tasks";
        return await db.LoadDataAsync<TaskModel, dynamic>(sql, new { }, _connectionString);
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        string sql = "SELECT * FROM dbo.tasks WHERE task_id = @Id";
        List<TaskModel> output = await db.LoadDataAsync<TaskModel, dynamic>(sql, new { Id = id }, _connectionString);
        return output.First();
    }

    public Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        throw new NotImplementedException();
    }
    
    public async Task CreateTaskAsync(TaskModel task)
    {
		string sql = "INSERT INTO dbo.tasks " +
            "(project_id, created_by, assigned_to, predessor_task, date_due, date_completed, task_name, task_description, status, priority) VALUES " +
            "(@ProjectId, @CreatedBy, @AssignedTo, @PredessorTask, @DateDue, @DateCompleted, @TaskName, @TaskDescription, @Staus, @Priority)";
		await db.SaveDataAsync(sql,
			new { task.ProjectId, task.CreatedBy, task.AssignedTo, task.PredessorTask, task.DateDue, task.DateCompleted, task.TaskName, task.TaskDescription, task.Status, task.Priority },
			_connectionString);
        return;
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        TaskModel oldTask = await GetTaskByIdAsync(task.TaskId);
        if (oldTask == null) {
			await CreateTaskAsync(task);
            return;
        }
        else
        {
            TaskModel updateTask = new TaskModel
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
            return;
		}
    }

    public async Task DeleteTaskAsync(int id)
    {
        string sql = "DELETE FROM dbo.tasks WHERE task_id = @Id";
        await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
        return;
    }
}