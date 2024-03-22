using Microsoft.EntityFrameworkCore.Update.Internal;
using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.Repositories.Interfaces
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> Search(string name);
        Task<IEnumerable<Faculty>> GetFaculties();
        Task<Faculty> GetFacultyByID(int Id);
        Task<Faculty> GetFacultyByName(string name);
        Task<Faculty> AddFaculty(Faculty faculty);
        Task<Faculty> UpdateFaculty(Faculty faculty);
        Task DeleteFaculty(int Id);


    }
}
