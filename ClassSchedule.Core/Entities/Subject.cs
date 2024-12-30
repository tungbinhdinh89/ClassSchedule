﻿namespace ClassSchedule.Core.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
    }
}
