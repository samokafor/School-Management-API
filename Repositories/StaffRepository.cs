using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories.Interfaces;

namespace SchoolManagementAPI.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly SchoolDbContext schoolDbContext;
        private readonly IMapper mapper;
        public StaffRepository(SchoolDbContext schoolDbContext, IMapper mapper)
        {
            this.schoolDbContext = schoolDbContext;
            this.mapper = mapper;
        }

        public async Task<StaffDto> AddNewStaffAsync(StaffDto staff)
        {
            if (staff == null) throw new ArgumentNullException(nameof(staff));
            var newStaff = new Staff
            {
                Title = staff.Title,
                FirstName = staff.FirstName,
                MiddleName = staff.MiddleName,
                LastName = staff.LastName,
                Gender = staff.Gender,
                DOB = staff.DOB,
                Email = staff.Email,
                PhoneNumber = staff.PhoneNumber,
                StaffGrade = staff.StaffGrade,
                DepartmentId = staff.DepartmentId,

            };
            var result = await schoolDbContext.Staff.AddAsync(newStaff);
            await schoolDbContext.SaveChangesAsync();
            return mapper.Map<StaffDto>(result.Entity);
        }

        public async Task DeleteStaffAsync(int Id)
        {
            var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == Id);
            if(staff == null) throw new Exception($"No Staff exists with ID {Id}");
            schoolDbContext.Staff.Remove(staff);
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
        {
            var staff = await schoolDbContext.Staff.ToListAsync();
            var staffDto = mapper.Map<IEnumerable<StaffDto>>(staff);
            return staffDto;
        }

        public async Task<StaffDto> GetStaffByEmailAsync(string email)
        {
            var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Email == email);
            if (staff == null)
            {
                return null;
            }
            var staffDto = mapper.Map<StaffDto>(staff);
            return staffDto;
        }

        public async Task<StaffDto> GetStaffByIDAsync(int Id)
        {
            var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == Id);
            if(staff == null)
            {
                return null;
            }
            var staffDto = mapper.Map<StaffDto>(staff);
            return staffDto;
        }

        public async Task<IEnumerable<StaffDto>> SearchAsync(string searchTerm)
        {
            IQueryable<Staff> query = schoolDbContext.Staff;

            if (!(string.IsNullOrEmpty(searchTerm)))
            {
                query = query.Where(s => s.FirstName.Contains(searchTerm) || s.MiddleName.Contains(searchTerm) || s.LastName.Contains(searchTerm));
            }
            var queryDto = mapper.Map<IEnumerable<StaffDto>>(query);
            return queryDto;
        }

        public async Task<StaffDto> UpdateStaffAsync(StaffDto staff)
        {
            var staffToUpdate = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == staff.Id);
            if (staffToUpdate == null)
            {
                return null;
                //throw new Exception($"The staff member with ID {staff.Id} does not exist!");
            }
            else
            {

                staffToUpdate.Title = staff.Title;
                staffToUpdate.FirstName = staff.FirstName;
                staffToUpdate.MiddleName = staff.MiddleName;
                staffToUpdate.LastName = staff.LastName;
                staffToUpdate.StaffGrade = staff.StaffGrade;
                staffToUpdate.Email = staffToUpdate.Email;
                staffToUpdate.DepartmentId = staffToUpdate.DepartmentId;
                staffToUpdate.DOB = staffToUpdate.DOB;
                staffToUpdate.Gender = staffToUpdate.Gender;

                await schoolDbContext.SaveChangesAsync();
                var staffDto = mapper.Map<StaffDto>(staffToUpdate);
                return staffDto;
            }
        }
    }
}
