using DataAccessLibrary.Entity;

namespace DataAccessLibrary.Repository.Interfaces
{
	public interface ITaskRepository
	{
		Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
		Task<TaskEntity> GetTaskByIdAsync(int id);
		Task<IEnumerable<TaskEntity>> GetTasksByProjectIdAsync(int projectID);
		Task<IEnumerable<TaskEntity>> GetTasksByAssignedUserIdAsync(int userID);
		Task CreateTaskAsync(TaskEntity task);
		Task UpdateTaskAsync(TaskEntity task);
		Task DeleteTaskAsync(int id);
	}
}