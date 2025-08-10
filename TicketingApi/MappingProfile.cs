using AutoMapper;
using DataAccessLibrary.Entity;
using TicketingApi.Models;

namespace TicketingApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Project
        CreateMap<ProjectEntity, ProjectModel>()
            .ForMember(dest => dest.ProjectDescription,
                opt => opt.MapFrom(src => src.ProjectDescription ?? string.Empty))
            .ReverseMap();

        // Task
        CreateMap<TaskEntity, TaskModel>().ReverseMap();

        // User
        CreateMap<UserEntity, UserModel>().ReverseMap();
    }
}


