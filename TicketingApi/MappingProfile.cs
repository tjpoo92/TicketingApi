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
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (Models.Status)src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (Models.Priority)src.Priority))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (DataAccessLibrary.Entity.Status)src.Status))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (DataAccessLibrary.Entity.Priority)src.Priority));

        // Task
        CreateMap<TaskEntity, TaskModel>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? (Models.Status?)src.Status.Value : null))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.HasValue ? (Models.Priority?)src.Priority.Value : null))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.HasValue ? (DataAccessLibrary.Entity.Status?)src.Status.Value : null))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.HasValue ? (DataAccessLibrary.Entity.Priority?)src.Priority.Value : null));

        // User
        CreateMap<UserEntity, UserModel>().ReverseMap();
    }
}


