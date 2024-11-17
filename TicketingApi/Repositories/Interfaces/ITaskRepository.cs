using TicketingApi.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<TaskModel> CreateTaskAsync(TaskModel task);
    Task UpdateTaskAsync(TaskModel task);
    Task DeleteTaskAsync(int id);
}