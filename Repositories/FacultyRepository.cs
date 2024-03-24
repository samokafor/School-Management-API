namespace SchoolManagementAPI.Repositories;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FacultyRepository : IFacultyRepository
{
    private readonly SchoolDbContext schoolDbContext;
    private readonly IMapper mapper;

    public FacultyRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        this.schoolDbContext = schoolDbContext;
        this.mapper = mapper;
    }
    public async Task<FacultyDto> AddFaculty(FacultyDto facultyDto)
    {
        var staff = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == facultyDto.DeanStaffId);
        if(staff == null)
        {
            throw new Exception($"No staff with id {facultyDto.DeanStaffId} exists!");
        }
        var newFaculty = new Faculty
        {
            Name = facultyDto.Name,
            FacultyCode = facultyDto.FacultyCode.ToUpper(),
            DeanStaffId = facultyDto.DeanStaffId,
            FacultyDean = $"{staff.Title} {staff.FirstName} {staff.MiddleName} {staff.LastName}"
        };

        var result = await schoolDbContext.Faculties.AddAsync(newFaculty);
        await schoolDbContext.SaveChangesAsync();

        // Map the newly added Faculty entity to a FacultyDto before returning
        return mapper.Map<FacultyDto>(result.Entity);
    }



    public async Task DeleteFaculty(int Id)
    {
        var result = await schoolDbContext.Faculties.FirstOrDefaultAsync(f => f.Id == Id);

        if(result != null)
        {
            schoolDbContext.Faculties.Remove(result);
            await schoolDbContext.SaveChangesAsync();   
        }
        else
        {
            throw new Exception($"No faculty with ID {Id}");
        }
    }

    public async Task<IEnumerable<FacultyDto>> GetFaculties()
    {
        
            var faculties= await schoolDbContext.Faculties
                .Include(f => f.Departments)
                .ToListAsync();
        var facultyDTOs = mapper.Map<IEnumerable<FacultyDto>>(faculties);
        return facultyDTOs;
    }

    public async Task<FacultyDto> GetFacultyByID(int Id)
    {
        var faculty = await schoolDbContext.Faculties
            .Include(f => f.Departments)
            .FirstOrDefaultAsync(f => f.Id == Id);

        if (faculty != null)
        {
            var facultyDTO = mapper.Map<FacultyDto>(faculty);
            return facultyDTO;
        }

        return null;
    }


    public async Task<FacultyDto> GetFacultyByName(string name)
    {
        var faculty = await schoolDbContext.Faculties
            .Include(f => f.Departments)
            .FirstOrDefaultAsync(f => f.Name == name);

        if (faculty != null)
        {
            var facultyDTO = mapper.Map<FacultyDto>(faculty);
            return facultyDTO;
        }

        return null;
    }


    public async Task<IEnumerable<FacultyDto>> Search(string searchString)
    {
        IQueryable<Faculty> query = schoolDbContext.Faculties.Include(f => f.Departments);

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(f => f.Name.Contains(searchString)|| f.FacultyCode.Contains(searchString));
        }
        var queryDto = mapper.Map<IEnumerable<FacultyDto>>(query);
        return queryDto;
    }

    public async Task<Faculty> UpdateFaculty(Faculty faculty)
    {
        var result = await schoolDbContext.Faculties.FirstOrDefaultAsync(f => f.Id == faculty.Id);
        if(result != null)
        {
            result.Name = faculty.Name;
            result.FacultyCode = faculty.FacultyCode;

            schoolDbContext.Update(result);
            await schoolDbContext.SaveChangesAsync();
            return result;
        }

        return null;
    }
}

  