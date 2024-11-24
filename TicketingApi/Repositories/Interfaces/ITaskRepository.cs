using TicketingApi.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    Task<TaskModel> GetTaskByIdAsync(int id);
    Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID);
    Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID);
    Task<TaskModel> CreateTaskAsync(TaskModel task);
    Task UpdateTaskAsync(TaskModel task);
    Task DeleteTaskAsync(int id);
}