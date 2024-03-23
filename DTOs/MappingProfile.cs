using AutoMapper;
using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.DTOs
{
    

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculty, FacultyDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<FacultyDto, Faculty>()
            .ForMember(dest => dest.Departments, opt => opt.Ignore()); // Ignore the department property
            CreateMap<Faculty, FacultyDto>();
        }
    }

}
