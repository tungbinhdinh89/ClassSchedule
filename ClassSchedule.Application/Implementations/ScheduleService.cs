using AutoMapper;
using ClassSchedule.Application.DTOs;
using ClassSchedule.Application.Interfaces;
using ClassSchedule.Application.Services;
using ClassSchedule.Core.DB;
using ClassSchedule.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassSchedule.Application.Implementations
{
    public class ScheduleService(ApplicationDbContext dbContext, IMapper mapper, LoggerService logger) : IScheduleService
    {
        public async Task CreateScheduleAsync(ScheduleDTO item)
        {
            var schedule = mapper.Map<Schedule>(item);
            dbContext.Schedules.Add(schedule);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Schedule created with ID: {ScheduleId}", schedule.Id);
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await dbContext.Schedules.FindAsync(id);
            if (schedule == null)
            {
                logger.LogWarning("Attempted to delete schedule with ID: {ScheduleId} but it was not found.", id);
                throw new Exception("Schedule not found");
            }

            dbContext.Schedules.Remove(schedule);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Schedule with ID: {ScheduleId} deleted successfully.", id);
            return true;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
        {
            var schedules = await dbContext.Schedules
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Location)
                .ToListAsync();
            logger.LogInformation("Retrieved {Count} schedules.", schedules.Count);
            return mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
        }

        public async Task<bool> UpdateScheduleAsync(ScheduleDTO item)
        {
            var schedule = await dbContext.Schedules.FindAsync(item.Id);
            if (schedule == null)
            {
                logger.LogError("Attempted to update schedule with ID: {ScheduleId} but it was not found.", item.Id);
                throw new Exception("Schedule not found");
            }


            mapper.Map(item, schedule);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Schedule with ID: {ScheduleId} updated successfully.", schedule.Id);
            return true;
        }
    }
}
