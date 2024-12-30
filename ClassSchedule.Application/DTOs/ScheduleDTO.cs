namespace ClassSchedule.Application.DTOs
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }


        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
    }
}
