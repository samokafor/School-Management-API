using SchoolManagementAPI.DTOs;
using System.Collections;

namespace SchoolManagementAPI.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Task<IEnumerable<StaffDto>> SearchAsync(string searchTerm);
        Task<IEnumerable<StaffDto>> GetAllStaffAsync();
        Task<StaffDto> AddNewStaffAsync(StaffDto staff);
        Task<StaffDto> GetStaffByIDAsync(int Id);
        Task<StaffDto> GetStaffByEmailAsync(string email);
        Task<StaffDto> UpdateStaffAsync(StaffDto staff);
        Task DeleteStaffAsync(int Id);


    }
}
