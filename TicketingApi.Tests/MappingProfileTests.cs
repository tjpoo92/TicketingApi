using AutoMapper;
using FluentAssertions;
using TicketingApi;
using Xunit;
using DataAccessLibrary.Entity;
using TicketingApi.Models;

namespace TicketingApi.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void ProjectEntity_To_Model_And_Back_RoundTrips()
    {
        var entity = new ProjectEntity
        {
            ProjectId = 1,
            CreatedBy = 2,
            ProjectName = "Proj",
            ProjectDescription = null,
            DateDue = DateTime.UtcNow.Date,
            DateCompleted = null,
            Priority = DataAccessLibrary.Entity.Priority.High,
            Status = DataAccessLibrary.Entity.Status.InReview,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UpdatedAt = DateTime.UtcNow
        };

        var model = _mapper.Map<ProjectModel>(entity);
        model.ProjectDescription.Should().NotBeNull();
        model.Priority.Should().Be(DataAccessLibrary.Entity.Priority.High);
        model.Status.Should().Be(DataAccessLibrary.Entity.Status.InReview);

        var back = _mapper.Map<ProjectEntity>(model);
        back.Priority.Should().Be(DataAccessLibrary.Entity.Priority.High);
        back.Status.Should().Be(DataAccessLibrary.Entity.Status.InReview);
    }

    [Fact]
    public void TaskEntity_To_Model_And_Back_Handles_Nullable_Enums()
    {
        var entity = new TaskEntity
        {
            TaskId = 10,
            ProjectId = 1,
            CreatedBy = null,
            AssignedTo = 5,
            DateDue = DateTime.UtcNow.Date,
            DateCompleted = null,
            TaskName = "Task A",
            TaskDescription = "Desc",
            Status = null,
            Priority = null,
            PredessorTask = null,
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            UpdatedAt = DateTime.UtcNow
        };

        var model = _mapper.Map<TaskModel>(entity);
        model.Status.Should().BeNull();
        model.Priority.Should().BeNull();

        model.Status = DataAccessLibrary.Entity.Status.Scoping;
        model.Priority = DataAccessLibrary.Entity.Priority.Low;

        var back = _mapper.Map<TaskEntity>(model);
        back.Status.Should().Be(DataAccessLibrary.Entity.Status.Scoping);
        back.Priority.Should().Be(DataAccessLibrary.Entity.Priority.Low);
    }

    [Fact]
    public void UserEntity_To_Model_And_Back_RoundTrips()
    {
        var entity = new UserEntity
        {
            UserId = 42,
            UserName = "Alice",
            UserEmail = "alice@example.com",
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow
        };

        var model = _mapper.Map<UserModel>(entity);
        model.UserId.Should().Be(42);
        model.UserName.Should().Be("Alice");

        var back = _mapper.Map<UserEntity>(model);
        back.UserEmail.Should().Be("alice@example.com");
    }
}


