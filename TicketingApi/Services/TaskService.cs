using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using TicketingApi.Models;

public class TaskService : ITaskService {
    private readonly TaskRepository _taskRepository;
    private readonly Validator _validator;

    public TaskService(TaskRepository taskRepository, Validator validator) {
        _taskRepository = taskRepository;
        _validator = validator;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        var tasksFromDatabase = await _taskRepository.GetAllTasksAsync();
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase)
        {
            tasks.Add(CopyToModel(task));
        }
        return tasks;
    }

    // public async Task<TaskModel> GetTaskByIdAsync(int id)
    // {
    //     _validator.ValidateId(id, "Task");
    //
    //     var task = await _taskRepository.GetTaskByIdAsync(id);
    //     _validator.ValidateObjectNotNull(task, "Task");
    //
    //     return CopyToModel(task);
    // }

    // public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    // {
    //     _validator.ValidateId(projectID, "Project");
    //     
    //     
    //     return await _taskRepository.GetTasksByProjectIdAsync(projectID);
    // }

    //     public async Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    // {
    //     _validator.ValidateId(userID, "User");
    //
    //     return await _taskRepository.GetTasksByUserIdAsync(userID);
    // }

    public async Task CreateTaskAsync(TaskModel task)
    {
        _validator.ValidateObjectNotNull(task, "Task");

        await _taskRepository.CreateTaskAsync(CopyToEntity(task));
    }

    // public async Task UpdateTaskAsync(TaskModel task)
    // {
    //     _validator.ValidateObjectNotNull(task, "Task");
    //
    //     var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);
    //     _validator.ValidateObjectNotNull(existingTask, "Task");
    //
    //     await _taskRepository.UpdateTaskAsync(CopyToEntity(task));
    // }
    //
    // public async Task DeleteTaskAsync(int id)
    // {
    //     _validator.ValidateId(id, "Task");
    //
    //     var existingTask = await _taskRepository.GetTaskByIdAsync(id);
    //     _validator.ValidateObjectNotNull(existingTask, "Task");
    //     
    //     await _taskRepository.DeleteTaskAsync(id);
    // }
    
    private static TaskModel CopyToModel(TaskEntity from)
    {
        TaskModel toModel = new TaskModel
        {
            TaskId = from.TaskId,
            CreatedBy = from.CreatedBy,
            TaskName = from.TaskName,
            TaskDescription = from.TaskDescription,
            DateDue = from.DateDue,
            DateCompleted = from.DateCompleted,
            Priority = from.Priority,
            Status = from.Status,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toModel;
    }
    
    private static TaskEntity CopyToEntity(TaskModel from)
    {
        TaskEntity toEntity = new TaskEntity
        {
            TaskId = from.TaskId,
            CreatedBy = from.CreatedBy,
            TaskName = from.TaskName,
            TaskDescription = from.TaskDescription,
            DateDue = from.DateDue,
            DateCompleted = from.DateCompleted,
            Priority = from.Priority,
            Status = from.Status,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toEntity;
    }
}