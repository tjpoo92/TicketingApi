using TicketingApi.Models;

public class TaskRepository : ITaskRepository
{
    public Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TaskModel> GetTaskByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskAsync(TaskModel task)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTaskAsync(int id)
    {
        throw new NotImplementedException();
    }
}