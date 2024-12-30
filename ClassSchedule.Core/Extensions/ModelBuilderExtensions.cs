using ClassSchedule.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassSchedule.Core.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Address = "04 Phạm Hùng, Quận 8" },
                new Location { Id = 2, Address = "30 đường số 8 KDC Đại Phúc, Bình Hưng, Bình Chánh" }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Trung tâm tiếng anh Kapla" },
                new Class { Id = 2, Name = "Trường mầm non Thuỷ Tiên 2" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Toán" },
                new Subject { Id = 2, Name = "Văn" },
                new Subject { Id = 3, Name = "Anh văn" },
                new Subject { Id = 4, Name = "Hội họa" },
                new Subject { Id = 5, Name = "Bóng đá" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "Nguyễn Văn A" },
                new Teacher { Id = 2, Name = "Trần Thị B" },
                new Teacher { Id = 3, Name = "Lê Văn C" },
                new Teacher { Id = 4, Name = "Hoàng Thị D" },
                new Teacher { Id = 5, Name = "Phạm Văn E" }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, Date = new DateTime(2024, 12, 27), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 30, 0), TeacherId = 1, SubjectId = 1, LocationId = 1, ClassId = 1 },
                new Schedule { Id = 2, Date = new DateTime(2024, 12, 27), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 30, 0), TeacherId = 2, SubjectId = 2, LocationId = 1, ClassId = 1 },
                new Schedule { Id = 3, Date = new DateTime(2024, 12, 28), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 30, 0), TeacherId = 3, SubjectId = 3, LocationId = 2, ClassId = 2 },
                new Schedule { Id = 4, Date = new DateTime(2024, 12, 28), StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(17, 30, 0), TeacherId = 4, SubjectId = 4, LocationId = 1, ClassId = 2 },
                new Schedule { Id = 5, Date = new DateTime(2024, 12, 29), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(9, 30, 0), TeacherId = 5, SubjectId = 5, LocationId = 2, ClassId = 1 }
            );
        }
    }
}
