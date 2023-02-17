using AutoMapper;
using TeamManagement.DTO;
using TeamManagement.Models;

namespace TeamManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
        }
    }
}
