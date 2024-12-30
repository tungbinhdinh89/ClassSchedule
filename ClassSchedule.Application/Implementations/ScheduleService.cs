using AutoMapper;
using ClassSchedule.Application.DTOs;
using ClassSchedule.Application.Interfaces;
using ClassSchedule.Application.Services;
using ClassSchedule.Application.Services.ClassSchedule.Application.Services;
using ClassSchedule.Core.DB;
using ClassSchedule.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassSchedule.Application.Implementations
{
    public class ScheduleService(ApplicationDbContext dbContext, IMapper mapper, TransientService transientService, SingletonService singletonService) : IScheduleService
    {
        public async Task CreateScheduleAsync(ScheduleRequestDTO item)
        {
            var schedule = mapper.Map<Schedule>(item);
            dbContext.Schedules.Add(schedule);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                throw new Exception("Schedule not found");
            }

            dbContext.Schedules.Remove(schedule);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
        {
            transientService.LogScheduleDetails("Fetching all schedules.");
            singletonService.LogGlobalActivity("Fetching all schedules globally.");

            var schedules = await dbContext.Schedules
                .Include(c => c.Class)
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Location)
                .ToListAsync();

            return mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
        }

        public async Task<bool> UpdateScheduleAsync(ScheduleRequestDTO item)
        {
            var schedule = await dbContext.Schedules.FindAsync(item.Id);
            if (schedule == null)
            {
                throw new Exception("Schedule not found");
            }


            mapper.Map(item, schedule);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
