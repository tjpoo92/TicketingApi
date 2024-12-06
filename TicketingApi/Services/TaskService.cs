using TicketingApi.Models;

public class TaskService : ITaskService {
    private readonly ITaskRepository _taskRepository;
    private readonly Validator _validator;

    public TaskService(ITaskRepository taskRepository, Validator validator) {
        _taskRepository = taskRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        _validator.ValidateId(id, "Task");

        var task = await _taskRepository.GetTaskByIdAsync(id);
        _validator.ValidateObjectNotNull(task, "Task");

        return task;
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        _validator.ValidateId(projectID, "Project");

        return await _taskRepository.GetTasksByProjectIdAsync(projectID);
    }

        public async Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        _validator.ValidateId(userID, "User");

        return await _taskRepository.GetTasksByUserIdAsync(userID);
    }

    public async Task<TaskModel> CreateTaskAsync(TaskModel task)
    {
        _validator.ValidateObjectNotNull(task, "Task");

        return await _taskRepository.CreateTaskAsync(task);
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        _validator.ValidateObjectNotNull(task, "Task");

        var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);
        _validator.ValidateObjectNotNull(existingTask, "Task");

        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        _validator.ValidateId(id, "Task");

        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        _validator.ValidateObjectNotNull(existingTask, "Task");
        
        await _taskRepository.DeleteTaskAsync(id);
    }
}