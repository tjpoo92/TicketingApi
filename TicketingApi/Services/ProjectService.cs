using DataAccessLibrary.Entity;
using DataAccessLibrary.Repository;
using TicketingApi.Models;

namespace TicketingApi.Services;

public class ProjectService : IProjectService {
	private readonly ProjectRepository _projectRepository;
	private readonly Validator _validator;
    private readonly AutoMapper.IMapper _mapper;

    public ProjectService(ProjectRepository projectRepository, Validator validator, AutoMapper.IMapper mapper) {
		_projectRepository = projectRepository;
		_validator = validator;
        _mapper = mapper;
	}

	public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync() {
		var projectsFromDatabase = await _projectRepository.GetAllProjectsAsync();
		List<ProjectModel> projects = [];
        projects.AddRange(projectsFromDatabase.Select(project => _mapper.Map<ProjectModel>(project)));
		return projects;
	}

	public async Task<ProjectModel> GetProjectByIdAsync(int id) {
	    // _validator.ValidateId(id, "Project");

	    var projectFromDatabase = await _projectRepository.GetProjectByIdAsync(id);
	    // _validator.ValidateObjectNotNull(projectFromDatabase, "Project");
	    
        return _mapper.Map<ProjectModel>(projectFromDatabase);
	}

	public async Task CreateProjectAsync(ProjectModel project)
	{
        await _projectRepository.CreateProjectAsync(_mapper.Map<ProjectEntity>(project));
	}

	public async Task UpdateProjectAsync(ProjectModel project)
	{
	    //_validator.ValidateObjectNotNull(project, "Project");

	    var existingProject = await _projectRepository.GetProjectByIdAsync(project.ProjectId);
	    //_validator.ValidateObjectNotNull(existingProject, "Project");
        
        await _projectRepository.UpdateProjectAsync(_mapper.Map<ProjectEntity>(project));
	}

	public async Task DeleteProjectAsync(int id)
	{
	    // _validator.ValidateId(id, "Project");
	    // Validation and cascade delete need to be completed
	    var existingProject = await _projectRepository.GetProjectByIdAsync(id);
	    
		await _projectRepository.DeleteProjectAsync(id);
	    
	    // _validator.ValidateObjectNotNull(existingProject, "Project");
	}

    private ProjectModel CopyToModel(ProjectEntity from) => _mapper.Map<ProjectModel>(from);
    private ProjectEntity CopyToEntity(ProjectModel from) => _mapper.Map<ProjectEntity>(from);
}