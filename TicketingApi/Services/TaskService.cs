using TicketingApi.Models;

public class TaskServiceValidator {
    // Validator for each method
    // Valid integer checks
    // Validate response objects aren't null
    // Validate any required fields


}

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

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectID);
        if (tasks == null) {
            throw new KeyNotFoundException("Tasks not found.");
        }
        return tasks;
    }

        public async Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        var tasks = await _taskRepository.GetTasksByUserIdAsync(userID);
        if (tasks == null) {
            throw new KeyNotFoundException("Tasks not found");
        }
        return tasks;
    }

    public async Task CreateTaskAsync(TaskModel task)
    {
        await _taskRepository.CreateTaskAsync(task);
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);
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