using SchoolManagementAPI.Database.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolManagementAPI.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CreditHours { get; set; }
        public string Level { get; set; }
        public string Semester { get; set; }
        public int? LecturerStaffId { get; set; }
        public List<string>? LecturerName { get; set; } = new List<string>();
        //public List<Staff>? Lecturer { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
