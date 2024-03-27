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
                MiddleName = staff.LastName,
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

        public Task DeleteStaffAsync(int Id)
        {
            throw new NotImplementedException();
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

        public Task<IEnumerable<StaffDto>> SearchAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<StaffDto> UpdateStaffAsync(StaffDto staff)
        {
            throw new NotImplementedException();
        }
    }
}
