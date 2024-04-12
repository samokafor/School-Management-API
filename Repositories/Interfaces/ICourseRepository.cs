using SchoolManagementAPI.DTOs;
using System.Collections;

namespace SchoolManagementAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseDto>> SearchAsync(string searchTerm);
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto> AddNewCourseAsync(CourseDto course);
        Task<CourseDto> GetCoursesById(int Id);
        Task<CourseDto> UpdateCourseAsync(CourseDto course);
        Task DeleteCourseAsync(int Id);
    }
}
