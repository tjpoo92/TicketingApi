using TicketingApi.Models;

public class TaskService : ITaskService {
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository) {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null) {
            throw new KeyNotFoundException("Task not found");
        }
        return task;
    }

    public async Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        return await _taskRepository.CreateTaskAsync(task);
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(task.task_id);
        if (existingTask == null) {
            throw new KeyNotFoundException("Task not found");
        }
        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        if (existingTask == null) {
            throw new KeyNotFoundException("Task not found");
        }
        await _taskRepository.DeleteTaskAsync(id);
    }
}