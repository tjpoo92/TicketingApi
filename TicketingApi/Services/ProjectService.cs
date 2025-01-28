using TicketingApi.Models;
using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using Priority = TicketingApi.Models.Priority;
using Status = TicketingApi.Models.Status;

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
		foreach (var project in projectsFromDatabase)
		{
			projects.Add(CopyToModel(project));
		}
		return projects;
	}

    //public async Task<ProjectModel> GetProjectByIdAsync(int id) {
    //    _validator.ValidateId(id, "Project");

    //    var project = await _projectRepository.GetProjectByIdAsync(id);
    //    _validator.ValidateObjectNotNull(project, "Project");

    //    return project;
    //}

    public void CreateProjectAsync(ProjectModel project)
    {
		_projectRepository.CreateProjectAsync(CopyToEntity(project));
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
		ProjectModel toModel = new ProjectModel();
		toModel.ProjectId = from.ProjectId;
		toModel.CreatedBy = from.CreatedBy;
		toModel.ProjectName = from.ProjectName;
		toModel.ProjectDescription = from.ProjectDescription;
		toModel.DateDue = from.DateDue;
		toModel.DateCompleted = from.DateCompleted;
		toModel.Priority = (Priority)from.Priority;
		toModel.Status = (Status)from.Status;
		toModel.CreatedAt = from.CreatedAt;
		toModel.UpdatedAt = from.UpdatedAt;
		return toModel;
	}

	private static ProjectEntity CopyToEntity(ProjectModel from)
	{
		ProjectEntity toEntity = new ProjectEntity();
		toEntity.ProjectId = from.ProjectId;
		toEntity.CreatedBy = from.CreatedBy;
		toEntity.ProjectName = from.ProjectName;
		toEntity.ProjectDescription = from.ProjectDescription;
		toEntity.DateDue = from.DateDue;
		toEntity.DateCompleted = from.DateCompleted;
		toEntity.Priority = (DataAccessLibrary.Entity.Priority)from.Priority;
		toEntity.Status = (DataAccessLibrary.Entity.Status)from.Status;
		toEntity.CreatedAt = from.CreatedAt;
		toEntity.UpdatedAt = from.UpdatedAt;
		return toEntity;
	}
}