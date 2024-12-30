using ClassSchedule.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassSchedule.Core.DB.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Date)
                   .IsRequired();

            builder.HasOne(s => s.Teacher)
                   .WithMany(t => t.Schedules)
                   .HasForeignKey(s => s.TeacherId);

            builder.HasOne(s => s.Subject)
                   .WithMany(sb => sb.Schedules)
                   .HasForeignKey(s => s.SubjectId);

            builder.HasOne(s => s.Location)
                   .WithMany(l => l.Schedules)
                   .HasForeignKey(s => s.LocationId);

            builder.HasOne(x => x.Class)
                   .WithMany(x => x.Schedules)
                   .HasForeignKey(x => x.ClassId);
        }
    }
}
