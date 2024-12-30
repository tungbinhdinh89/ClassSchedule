namespace ClassSchedule.Application.DTOs
{
    public class ScheduleRequestDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int LocationId { get; set; }
    }
}
