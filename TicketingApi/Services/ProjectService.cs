using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using TicketingApi.Models;
using Priority = TicketingApi.Models.Priority;
using Status = TicketingApi.Models.Status;

namespace TicketingApi.Services;

public class ProjectService : IProjectService {
	private readonly ProjectRepository _projectRepository;
	private readonly Validator _validator;

	public ProjectService(ProjectRepository projectRepository, Validator validator) {
		_projectRepository = projectRepository;
		_validator = validator;
	}

	public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync() {
		var projectsFromDatabase = await _projectRepository.GetAllProjectsAsync();
		List<ProjectModel> projects = [];
		projects.AddRange(projectsFromDatabase.Select(project => CopyToModel(project)));
		return projects;
	}

	public async Task<ProjectModel> GetProjectByIdAsync(int id) {
	    // _validator.ValidateId(id, "Project");

	    var projectFromDatabase = await _projectRepository.GetProjectByIdAsync(id);
	    // _validator.ValidateObjectNotNull(projectFromDatabase, "Project");
	    
	    return CopyToModel(projectFromDatabase);
	}

	public async Task CreateProjectAsync(ProjectModel project)
	{
		await _projectRepository.CreateProjectAsync(CopyToEntity(project));
	}

	//public async Task UpdateProjectAsync(ProjectModel project)
	//{
	//    _validator.ValidateObjectNotNull(project, "Project");

	//    var existingProject = await _projectRepository.GetProjectByIdAsync(project.ProjectId);
	//    _validator.ValidateObjectNotNull(existingProject, "Project");
        
	//    await _projectRepository.UpdateProjectAsync(project);
	//}

	//public async Task DeleteProjectAsync(int id)
	//{
	//    _validator.ValidateId(id, "Project");

	//    var existingProject = await _projectRepository.GetProjectByIdAsync(id);
	//    _validator.ValidateObjectNotNull(existingProject, "Project");
        
	//    await _projectRepository.DeleteProjectAsync(id);
	//}

	private static ProjectModel CopyToModel(ProjectEntity from)
	{
		ProjectModel toModel = new ProjectModel
		{
			ProjectId = from.ProjectId,
			CreatedBy = from.CreatedBy,
			ProjectName = from.ProjectName,
			ProjectDescription = from.ProjectDescription ?? "",
			DateDue = from.DateDue,
			DateCompleted = from.DateCompleted,
			Priority = (Priority)from.Priority,
			Status = (Status)from.Status,
			CreatedAt = from.CreatedAt,
			UpdatedAt = from.UpdatedAt
		};
		return toModel;
	}

	private static ProjectEntity CopyToEntity(ProjectModel from)
	{
		ProjectEntity toEntity = new ProjectEntity
		{
			ProjectId = from.ProjectId,
			CreatedBy = from.CreatedBy,
			ProjectName = from.ProjectName,
			ProjectDescription = from.ProjectDescription,
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