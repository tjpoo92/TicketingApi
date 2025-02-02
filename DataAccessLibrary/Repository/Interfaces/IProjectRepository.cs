using DataAccessLibrary.Entity;

namespace DataAccessLibrary.Repository.Interfaces
{
	public interface IProjectRepository
	{
		Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
		Task<ProjectEntity> GetProjectByIdAsync(int id);
		Task CreateProjectAsync(ProjectEntity project);
		//Task UpdateProjectAsync(ProjectEntity project);
		//Task DeleteProjectAsync(int id);
	}
}