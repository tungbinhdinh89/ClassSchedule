using AutoMapper;
using ClassSchedule.Application.DTOs;
using ClassSchedule.Application.Implementations;
using ClassSchedule.Application.Services;
using ClassSchedule.Application.Services.ClassSchedule.Application.Services;
using ClassSchedule.Core.DB;
using ClassSchedule.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClassSchedule.UnitTests
{
    public class ScheduleServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<TransientService> _transientServiceMock;
        private readonly Mock<SingletonService> _singletonServiceMock;
        private readonly ScheduleService _scheduleService;

        public ScheduleServiceTests()
        {
            _mapperMock = new Mock<IMapper>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _transientServiceMock = new Mock<TransientService>();
            _singletonServiceMock = new Mock<SingletonService>();

            _scheduleService = new ScheduleService(
                _dbContext,
                _mapperMock.Object,
                _transientServiceMock.Object,
                _singletonServiceMock.Object
            );
        }

        [Fact]
        public async Task CreateScheduleAsync_ValidRequest_AddsScheduleToDatabase()
        {
            // Arrange
            var scheduleRequest = new ScheduleRequestDTO
            {
                Id = 1,
                ClassId = 101,
                SubjectId = 201,
                TeacherId = 301,
                LocationId = 401,
                Date = DateTime.Now,
            };

            var scheduleEntity = new Schedule
            {
                Id = 1,
                ClassId = 101,
                SubjectId = 201,
                TeacherId = 301,
                LocationId = 401,
                Date = scheduleRequest.Date,
            };

            _mapperMock.Setup(m => m.Map<Schedule>(It.IsAny<ScheduleRequestDTO>()))
                       .Returns(scheduleEntity);

            // Act
            await _scheduleService.CreateScheduleAsync(scheduleRequest);

            // Assert
            var savedSchedule = _dbContext.Schedules.FirstOrDefault(s => s.Id == scheduleEntity.Id);
            Assert.NotNull(savedSchedule);
            Assert.Equal(scheduleEntity.ClassId, savedSchedule.ClassId);
            Assert.Equal(scheduleEntity.SubjectId, savedSchedule.SubjectId);
            Assert.Equal(scheduleEntity.TeacherId, savedSchedule.TeacherId);
            Assert.Equal(scheduleEntity.LocationId, savedSchedule.LocationId);
            Assert.Equal(scheduleEntity.Date, savedSchedule.Date);
            _mapperMock.Verify(m => m.Map<Schedule>(It.IsAny<ScheduleRequestDTO>()), Times.Once);
        }

        [Fact]
        public async Task DeleteScheduleAsync_ValidId_RemovesScheduleFromDatabase()
        {
            // Arrange
            var schedule = new Schedule { Id = 1, ClassId = 101 };
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _scheduleService.DeleteScheduleAsync(schedule.Id);

            // Assert
            Assert.True(result);
            Assert.Null(_dbContext.Schedules.FirstOrDefault(s => s.Id == schedule.Id));
        }

        [Fact]
        public async Task GetAllScheduleAsync_WhenCalled_ReturnsAllSchedules()
        {
            // Arrange
            var schedules = new List<Schedule>
            {
                new Schedule { Id = 1,ClassId= 101},
                new Schedule { Id = 2,ClassId = 102 }
            };

            _dbContext.Schedules.AddRange(schedules);
            await _dbContext.SaveChangesAsync();

            _mapperMock.Setup(m => m.Map<IEnumerable<ScheduleDTO>>(It.IsAny<IEnumerable<Schedule>>()))
                        .Returns(schedules.Select(s => new ScheduleDTO { Id = s.Id }));

            // Act
            var result = await _scheduleService.GetAllSchedulesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateScheduleAsync_ValidRequest_UpdatesScheduleInDatabase()
        {
            // Arrange
            var schedule = new Schedule { Id = 1, ClassId = 101 };
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();

            var updateRequest = new ScheduleRequestDTO { Id = 1, ClassId = 102 };

            _mapperMock.Setup(m => m.Map(It.IsAny<ScheduleRequestDTO>(), It.IsAny<Schedule>()))
                        .Callback<ScheduleRequestDTO, Schedule>((src, dest) => dest.ClassId = src.ClassId);

            // Act
            var result = await _scheduleService.UpdateScheduleAsync(updateRequest);

            // Assert
            Assert.True(result);
            var updateSchedule = _dbContext.Schedules.FirstOrDefault(s => s.Id == 1);
            Assert.NotNull(updateSchedule);
            Assert.Equal(102, updateSchedule.ClassId);
        }
    }
}