using ClassSchedule.Application.DTOs;

namespace ClassSchedule.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();
        Task CreateScheduleAsync(ScheduleDTO schedule);
        Task<bool> UpdateScheduleAsync(ScheduleDTO schedule);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
