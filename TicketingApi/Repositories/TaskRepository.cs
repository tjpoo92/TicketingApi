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
    
    public Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskAsync(TaskModel task)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTaskAsync(int id)
    {
        string sql = "DELETE FROM dbo.tasks WHERE task_id = @Id";
        await db.SaveDataAsync(sql, new { Id = id }, _connectionString);
    }
}