using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using DataAccessLibrary.Repository.Interfaces;
using TicketingApi.Models;
using TicketingApi.Services.Interfaces;

namespace TicketingApi.Services;

public class TaskService : ITaskService {
    private readonly ITaskRepository _taskRepository;
    private readonly Validator _validator;
    private readonly AutoMapper.IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, Validator validator, AutoMapper.IMapper mapper) {
        _taskRepository = taskRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
    {
        var tasksFromDatabase = await _taskRepository.GetAllTasksAsync();
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase)
        {
            tasks.Add(_mapper.Map<TaskModel>(task));
        }
        return tasks;
    }

    public async Task<TaskModel?> GetTaskByIdAsync(int id)
    {
        _validator.ValidateId(id, "Task");
    
        var task = await _taskRepository.GetTaskByIdAsync(id);
        _validator.ValidateObjectNotNull(task, "Task");
    
        return _mapper.Map<TaskModel>(task);
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByProjectIdAsync(int projectID)
    {
        //_validator.ValidateId(projectID, "Project");
        
        var tasksFromDatabase = await _taskRepository.GetTasksByProjectIdAsync(projectID);
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase) {
            tasks.Add(_mapper.Map<TaskModel>(task));
        }
        return tasks;
    }

    public async Task<IEnumerable<TaskModel>> GetTasksByUserIdAsync(int userID)
    {
        //_validator.ValidateId(userID, "User");
    
        var tasksFromDatabase = await _taskRepository.GetTasksByAssignedUserIdAsync(userID);
        List<TaskModel> tasks = [];
        foreach (var task in tasksFromDatabase) {
            tasks.Add(_mapper.Map<TaskModel>(task));
        }
        return tasks;
    }

    public async Task CreateTaskAsync(TaskModel task)
    {
        //_validator.ValidateObjectNotNull(task, "Task");

        await _taskRepository.CreateTaskAsync(_mapper.Map<TaskEntity>(task));
    }

    public async Task UpdateTaskAsync(TaskModel task)
    {
        //_validator.ValidateObjectNotNull(task, "Task");
    
        var existingTask = await _taskRepository.GetTaskByIdAsync(task.TaskId);
        //_validator.ValidateObjectNotNull(existingTask, "Task");
    
        await _taskRepository.UpdateTaskAsync(_mapper.Map<TaskEntity>(task));
    }
    
    public async Task DeleteTaskAsync(int id)
    {
        // _validator.ValidateId(id, "Task");
    
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);
        //_validator.ValidateObjectNotNull(existingTask, "Task");
        
        await _taskRepository.DeleteTaskAsync(id);
    }
    
    private TaskModel CopyToModel(TaskEntity from) => _mapper.Map<TaskModel>(from);
    private TaskEntity CopyToEntity(TaskModel from) => _mapper.Map<TaskEntity>(from);
}