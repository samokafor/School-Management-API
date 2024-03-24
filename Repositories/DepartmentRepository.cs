using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories.Interfaces;
using System.Collections.Generic;

namespace SchoolManagementAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SchoolDbContext schoolDbContext;
        private readonly IMapper mapper;
        private readonly IFacultyRepository facultyRepository;

        public DepartmentRepository(SchoolDbContext schoolDbContext, IMapper mapper, IFacultyRepository facultyRepository)
        {
            this.schoolDbContext = schoolDbContext;
            this.mapper = mapper;
            this.facultyRepository = facultyRepository;
        }

        public async Task<DepartmentDto> AddNewDepartmentAsync(DepartmentDto department)
        {
            if (await facultyRepository.GetFacultyByID(department.FacultyID) == null )
            {
                throw new Exception($"No Faculty exists with ID {department.FacultyID}");
            }
            var newDepartment = new Department
            {
                Name = department.Name,
                DepartmentCode = department.DepartmentCode.ToUpper(),
                FacultyId = department.FacultyID
            };
            var result = await schoolDbContext.Departments.AddAsync(newDepartment);
            schoolDbContext.SaveChanges();
            return mapper.Map<DepartmentDto>(result.Entity);
        }

        public async Task DeleteDepartmentAsync(int Id)
        {
            var result = await schoolDbContext.Departments.FirstOrDefaultAsync(d => d.Id == Id);
            if (result != null)
            {
                schoolDbContext.Departments.Remove(result);
                await schoolDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"No department exists with ID {Id}");
            }
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var department = await schoolDbContext.Departments.ToListAsync();
            var departmentDto = mapper.Map<IEnumerable<DepartmentDto>>(department);
            return departmentDto;
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int Id)
        {
            var department = await schoolDbContext.Departments.FirstOrDefaultAsync(d => d.Id == Id);
            if (department != null)
            {
                var departmentDto = mapper.Map<DepartmentDto>(department);
                return departmentDto;
            }
            else
            {
                throw new Exception($"No department exists with ID {Id}");
            }
            
        }

        public async Task<DepartmentDto> GetDepartmentByNameAsync(string name)
        {
            var department = await schoolDbContext.Departments.FirstOrDefaultAsync(d => d.Name == name);
            if (department != null)
            {
                var departmentDto = mapper.Map<DepartmentDto>(department);
                return departmentDto;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<DepartmentDto>> SearchAsync(string searchTerm)
        {
            IQueryable<Department> query = schoolDbContext.Departments;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(d => d.Name.Contains(searchTerm) || d.DepartmentCode.Contains(searchTerm));
                

            }
            var queryDto = mapper.Map<IEnumerable<DepartmentDto>>(query);
            return  queryDto;
        }

        public async Task<DepartmentDto> UpdateDepartment(DepartmentDto department)
        {
            var departmentToUpdate = await schoolDbContext.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
            if (departmentToUpdate != null)
            {
                departmentToUpdate.Name = department.Name;
                departmentToUpdate.DepartmentCode = department.DepartmentCode;
                departmentToUpdate.FacultyId = department.FacultyID;

                schoolDbContext.Update(departmentToUpdate);
                await schoolDbContext.SaveChangesAsync();
                return mapper.Map<DepartmentDto>(departmentToUpdate);
            }
            return null;
        }
    }
}
