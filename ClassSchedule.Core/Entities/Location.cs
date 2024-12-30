namespace ClassSchedule.Core.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;

        public ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
    }

}
