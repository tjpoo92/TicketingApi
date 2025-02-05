using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using TicketingApi.Models;
using Priority = TicketingApi.Models.Priority;
using Status = TicketingApi.Models.Status;

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

    public async Task<TaskModel> GetTaskByIdAsync(int id)
    {
        _validator.ValidateId(id, "Task");
    
        var task = await _taskRepository.GetTaskByIdAsync(id);
        _validator.ValidateObjectNotNull(task, "Task");
    
        return CopyToModel(task);
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        //_validator.ValidateId(projectID, "Project");
        
        var tasksFromDatabase = await _taskRepository.GetTasksByProjectIdAsync(projectID);
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase) {
            tasks.Add(CopyToModel(task));
        }
        return tasks;
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        //_validator.ValidateId(userID, "User");
    
        var tasksFromDatabase = await _taskRepository.GetTasksByAssignedUserIdAsync(userID);
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase) {
            tasks.Add(CopyToModel(task));
        }
        return tasks;
    }

    public async Task CreateTaskAsync(TaskModel task)
    {
        //_validator.ValidateObjectNotNull(task, "Task");

        await _taskRepository.CreateTaskAsync(CopyToEntity(task));
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        //_validator.ValidateObjectNotNull(task, "Task");
    
        var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);
        //_validator.ValidateObjectNotNull(existingTask, "Task");
    
        await _taskRepository.UpdateTaskAsync(CopyToEntity(task));
    }
    
    public async Task DeleteTaskAsync(int id)
    {
        // _validator.ValidateId(id, "Task");
    
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        //_validator.ValidateObjectNotNull(existingTask, "Task");
        
        await _taskRepository.DeleteTaskAsync(id);
    }
    
    private static TaskModel CopyToModel(TaskEntity from)
    {
        TaskModel toModel = new TaskModel
        {
            ProjectId = from.ProjectId,
            TaskId = from.TaskId,
            CreatedBy = from.CreatedBy,
            AssignedTo = from.AssignedTo,
            PredessorTask = from.PredessorTask,
            TaskName = from.TaskName,
            TaskDescription = from.TaskDescription,
            DateDue = from.DateDue,
            DateCompleted = from.DateCompleted,
            Priority = (Priority)from.Priority,
            Status = (Status)from.Status,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toModel;
    }
    
    private static TaskEntity CopyToEntity(TaskModel from)
    {
        TaskEntity toEntity = new TaskEntity
        {
            ProjectId = from.ProjectId,
            TaskId = from.TaskId,
            CreatedBy = from.CreatedBy,
            AssignedTo = from.AssignedTo,
            PredessorTask = from.PredessorTask,
            TaskName = from.TaskName,
            TaskDescription = from.TaskDescription,
            DateDue = from.DateDue,
            DateCompleted = from.DateCompleted,
            Priority = (DataAccessLibrary.Entity.Priority)from.Priority,
            Status = (DataAccessLibrary.Entity.Status)from.Status,
            CreatedAt = from.CreatedAt,
            UpdatedAt = from.UpdatedAt
        };
        return toEntity;
    }
}