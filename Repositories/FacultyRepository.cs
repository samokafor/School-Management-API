namespace SchoolManagementAPI.Repositories;

using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.Repositories.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public class FacultyRepository : IFacultyRepository
{
    private readonly SchoolDbContext schoolDbContext;

    public FacultyRepository(SchoolDbContext schoolDbContext)
    {
        this.schoolDbContext = schoolDbContext;
    }
    public async Task<Faculty> AddFaculty(Faculty faculty)
    {
        var result = await schoolDbContext.Faculties.AddAsync(faculty);
        await schoolDbContext.SaveChangesAsync();
        return result.Entity;
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

    public async Task<IEnumerable<Faculty>> GetFaculties()
    {
        return await schoolDbContext.Faculties.ToListAsync();
    }

    public async Task<Faculty> GetFacultyByID(int Id)
    {
        var result = await schoolDbContext.Faculties.FirstOrDefaultAsync(f => f.Id == Id);
        if (result != null)
        {
            return result;
        }

        return null;
    }

    public async Task<Faculty> GetFacultyByName(string name)
    {
        var result = await schoolDbContext.Faculties.FirstOrDefaultAsync(f => f.Name == name);
        if (result != null)
        {
            return result;
        }

        return null;
    }

    public async Task<IEnumerable<Faculty>> Search(string searchString)
    {
        IQueryable<Faculty> query = schoolDbContext.Faculties;

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(f => f.Name.Contains(searchString)|| f.FacultyCode.Contains(searchString));
        }
        
        return await query.ToListAsync();
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

  