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
			ProjectModel projectModel = new ProjectModel();
			CopyToModel(project, projectModel);
			projects.Add(projectModel);
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
		ProjectEntity projectEntity = new ProjectEntity();
		_projectRepository.CreateProjectAsync(CopyToEntity(project, projectEntity));
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

	private static ProjectModel CopyToModel(ProjectEntity from, ProjectModel to)
	{
		to.ProjectId = from.ProjectId;
		to.CreatedBy = from.CreatedBy;
		to.ProjectName = from.ProjectName;
		to.ProjectDescription = from.ProjectDescription;
		to.DateDue = from.DateDue;
		to.DateCompleted = from.DateCompleted;
		to.Priority = (Priority)from.Priority;
		to.Status = (Status)from.Status;
		to.CreatedAt = from.CreatedAt;
		to.UpdatedAt = from.UpdatedAt;
		return to;
	}

	private static ProjectEntity CopyToEntity(ProjectModel from, ProjectEntity to)
	{
		to.ProjectId = from.ProjectId;
		to.CreatedBy = from.CreatedBy;
		to.ProjectName = from.ProjectName;
		to.ProjectDescription = from.ProjectDescription;
		to.DateDue = from.DateDue;
		to.DateCompleted = from.DateCompleted;
		to.Priority = (DataAccessLibrary.Entity.Priority)from.Priority;
		to.Status = (DataAccessLibrary.Entity.Status)from.Status;
		to.CreatedAt = from.CreatedAt;
		to.UpdatedAt = from.UpdatedAt;
		return to;
	}
}