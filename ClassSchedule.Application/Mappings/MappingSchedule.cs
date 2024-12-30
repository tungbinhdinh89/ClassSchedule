using AutoMapper;
using ClassSchedule.Application.DTOs;
using ClassSchedule.Core.Entities;

namespace ClassSchedule.Application.Mappings
{
    public class MappingSchedule : Profile
    {
        public MappingSchedule()
        {
            // Map Schedule -> ScheduleDTO
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.StartTime.Days))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.Hours))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.Hours))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject!.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher!.Name))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location!.Address));


            // Map ScheduleDTO -> Schedule
            CreateMap<ScheduleDTO, Schedule>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Date.Add(src.StartTime)))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Date.Add(src.EndTime)))
                .ForMember(dest => dest.Subject, opt => opt.Ignore())
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ForMember(dest => dest.Location, opt => opt.Ignore());
        }
    }
}
