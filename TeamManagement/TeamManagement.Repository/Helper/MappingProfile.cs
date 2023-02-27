using AutoMapper;
using System.Runtime.Intrinsics.Arm;
using TeamManagement.DTO;
using TeamManagement.Models;
using TeamManagement.RequestBodyModel;

namespace TeamManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Team, TeamDTO>().ReverseMap();    
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Admin, AdminDTO>().ReverseMap();  
            CreateMap<Semester, SemesterDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>().ReverseMap();
            CreateMap<Subject, SubjectDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Course, TeacherCourseModel>().ReverseMap();
        }
    }
}
