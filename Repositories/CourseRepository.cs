using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database;
using SchoolManagementAPI.Database.Models;
using SchoolManagementAPI.DTOs;
using SchoolManagementAPI.Repositories.Interfaces;

namespace SchoolManagementAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ICourseRepository courseRepository;
        private readonly SchoolDbContext schoolDbContext;
        private readonly Mapper mapper;

        public CourseRepository(ICourseRepository courseRepository, SchoolDbContext schoolDbContext, Mapper mapper)
        {
            this.courseRepository = courseRepository;
            this.schoolDbContext = schoolDbContext;
            this.mapper = mapper;
        }

        public async Task<CourseDto> AddNewCourseAsync(CourseDto course)
        {
            if (course == null) { return null; }

            var courseCode = course.Department.DepartmentCode + course.Level[0] + course.Semester[0] + course.Id;
            var lecturer = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == course.LecturerStaffId);
            var lecturerName = $"{lecturer?.Title} {lecturer?.FirstName} {lecturer?.MiddleName} {lecturer?.LastName}";
            var newCourse = new Course();

            newCourse.CourseCode = courseCode;
            newCourse.CourseTitle = course.CourseTitle;
            newCourse.CreditHours = course.CreditHours;
            newCourse.LecturerStaffId = course.LecturerStaffId;
            newCourse.DepartmentId = course.DepartmentId;
            newCourse.LecturerName?.Add(lecturerName);

            
            var result = await schoolDbContext.Courses.AddAsync(newCourse);
            await schoolDbContext.SaveChangesAsync();
            return mapper.Map<CourseDto>(result.Entity);

        }

        public async Task DeleteCourseAsync(int Id)
        {
            var course  = await schoolDbContext.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null) { return; }
            schoolDbContext.Courses.Remove(course);
            await schoolDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await schoolDbContext.Courses.ToListAsync();
            var courseDto = mapper.Map<IEnumerable<CourseDto>>(courses);
            return courseDto;
        }

        public async Task<CourseDto> GetCoursesById(int Id)
        {
            var course = await schoolDbContext.Courses.FirstOrDefaultAsync(c => c.Id == Id);
            if (course == null) return null;
            var courseDto = mapper.Map<CourseDto>(course);
            return courseDto;


        }

        public async Task<IEnumerable<CourseDto>> SearchAsync(string searchTerm)
        {
            IQueryable<Course> query = schoolDbContext.Courses;

            if(!(string.IsNullOrEmpty(searchTerm)))
            {
                query = query.Where(c => c.CourseTitle == searchTerm || c.CourseCode == searchTerm);
            }
            var queryDto = mapper.Map<IEnumerable<CourseDto>>(query);
            return queryDto;
        }

        public async Task<CourseDto> UpdateCourseAsync(CourseDto course)
        {
            var courseToUpdate  = await schoolDbContext.Courses.FirstOrDefaultAsync(c => c.Id == course.Id);
            if(courseToUpdate == null)
            {
                return null;
            }

            var courseCode = course.Department.DepartmentCode + course.Level[0] + course.Semester[0] + course.Id;
            var lecturer = await schoolDbContext.Staff.FirstOrDefaultAsync(s => s.Id == course.LecturerStaffId);
            var lecturerName = $"{lecturer?.Title} {lecturer?.FirstName} {lecturer?.MiddleName} {lecturer?.LastName}";
            var newCourse = new Course();

            newCourse.CourseCode = courseCode;
            newCourse.CourseTitle = course.CourseTitle;
            newCourse.CreditHours = course.CreditHours;
            newCourse.LecturerStaffId = course.LecturerStaffId;
            newCourse.DepartmentId = course.DepartmentId;
            newCourse.LecturerName?.Add(lecturerName);


            var result = await schoolDbContext.Courses.AddAsync(newCourse);
            await schoolDbContext.SaveChangesAsync();
            return mapper.Map<CourseDto>(result.Entity);
        }
    }
}
