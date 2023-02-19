using AutoMapper;
using System.Runtime.Intrinsics.Arm;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
