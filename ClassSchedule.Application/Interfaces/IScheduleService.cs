using ClassSchedule.Application.DTOs;

namespace ClassSchedule.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();
        Task CreateScheduleAsync(ScheduleRequestDTO schedule);
        Task<bool> UpdateScheduleAsync(ScheduleRequestDTO schedule);
        Task<bool> DeleteScheduleAsync(int id);
    }
}
