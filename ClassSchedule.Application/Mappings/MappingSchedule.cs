using AutoMapper;
using ClassSchedule.Application.DTOs;
using ClassSchedule.Core.Entities;

namespace ClassSchedule.Application.Mappings
{
    public class MappingSchedule : Profile
    {
        public MappingSchedule()
        {
            //CreateMap<Schedule, ScheduleDTO>().ReverseMap();

            // Map Schedule -> ScheduleDTO
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(x => x.Class!.Name))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(x => x.Subject!.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(x => x.Teacher!.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(x => x.Location!.Address));

            // Map ScheduleDTO -> Schedule
            CreateMap<ScheduleRequestDTO, Schedule>()
                  .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ClassId))
                  .ForMember(dest => dest.SubjectId, opt => opt.MapFrom(src => src.SubjectId))
                  .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId))
                  .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.LocationId))
                  .ForMember(dest => dest.Class, opt => opt.Ignore())
                  .ForMember(dest => dest.Subject, opt => opt.Ignore())
                  .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                  .ForMember(dest => dest.Location, opt => opt.Ignore());
        }
    }
}
