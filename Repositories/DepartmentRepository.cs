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
            if (await facultyRepository.GetFacultyByID(department.FacultyID) == null)
            {
                throw new Exception($"No Faculty exists with ID {department.FacultyID}");
            }
            var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(d => d.Id == department.HeadOfDepartmentStaffId);
            if (staff == null)
            {
                throw new Exception($"No staff with id {department.HeadOfDepartmentStaffId} exists!");
            }
            var hod = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == department.HeadOfDepartmentStaffId);

            var newDepartment = new Department
            {
                Name = department.Name,
                DepartmentCode = department.DepartmentCode.ToUpper(),
                HeadOfDepartmentStaffId = department.HeadOfDepartmentStaffId,
                HeadOfDepartment = hod != null ? $"{hod.Title}. {hod.FirstName} {hod.MiddleName} {hod.LastName}" : "Not Yet Appointed",
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
            var department = await schoolDbContext.Departments.Include(d => d.StaffMembers).ToListAsync();
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
            var department = await schoolDbContext.Departments.Include(d => d.StaffMembers).FirstOrDefaultAsync(d => d.Name == name);
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
            IQueryable<Department> query = schoolDbContext.Departments.Include(d => d.StaffMembers);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(d => d.Name.Contains(searchTerm) || d.DepartmentCode.Contains(searchTerm));


            }
            var queryDto = mapper.Map<IEnumerable<DepartmentDto>>(query);
            return queryDto;
        }

        public async Task<DepartmentDto> UpdateDepartment(DepartmentDto department)
        {
            var departmentToUpdate = await schoolDbContext.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);
            if (departmentToUpdate != null)
            {
                Staff? hod;
                try
                {
                    hod = department.HeadOfDepartmentStaffId != 0 ? 
                        await schoolDbContext.Staff.FirstOrDefaultAsync
                        (
                            s => s.Id == department.HeadOfDepartmentStaffId 
                            && s.DepartmentId == department.Id
                         ) 
                            : null;
                    
                    departmentToUpdate.HeadOfDepartmentStaffId = hod?.Id;
                    departmentToUpdate.HeadOfDepartment = hod != null ? $"{hod.Title}. {hod.FirstName} {hod.MiddleName} {hod.LastName}" 
                        : "Not Yet Appointed";
                
                }
                catch ( Exception ex )
                {
                    hod = null;
                }
                departmentToUpdate.Name = department.Name;
                departmentToUpdate.DepartmentCode = department.DepartmentCode;
                departmentToUpdate.FacultyId = department.FacultyID;
                schoolDbContext.Update(departmentToUpdate);
                await schoolDbContext.SaveChangesAsync();
                var updatedDepartment = await GetDepartmentByIdAsync(departmentToUpdate.Id);
                return mapper.Map<DepartmentDto>(updatedDepartment);
            }
            return null;
        }


        public async Task<bool> CheckStaffInDepartmentAsync(int Id, DepartmentDto department)
        {
            var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == department.HeadOfDepartmentStaffId && s.DepartmentId == department.Id);
            if (staff == null)
            {
                throw new Exception($"The staff member with ID {Id} is not assigned to the {department.Name} department.");
            } 
            else 
            {
                return true; }
            }

}
        }


    


