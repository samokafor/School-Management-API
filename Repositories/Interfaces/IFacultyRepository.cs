using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;

namespace SchoolManagementAPI.Repositories.Interfaces
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> Search(string name);
        Task<IEnumerable<FacultyDto>> GetFaculties();
        Task<FacultyDto> GetFacultyByID(int Id);
        Task<FacultyDto> GetFacultyByName(string name);
        Task<FacultyDto> AddFaculty(FacultyDto faculty);
        Task<Faculty> UpdateFaculty(Faculty faculty);
        Task DeleteFaculty(int Id);


    }
}
