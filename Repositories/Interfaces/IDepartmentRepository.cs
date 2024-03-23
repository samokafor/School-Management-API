using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using System.Collections.Generic;

namespace SchoolManagementAPI.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> SearchAsync(string searchTerm);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int Id);
        Task<DepartmentDto> GetDepartmentByNameAsync(string name);
        Task<DepartmentDto> AddNewDepartmentAsync(DepartmentDto department);
        Task<Department> UpdateDepartment(Department department);
        Task DeleteDepartmentAsync(int Id);
    }
}
