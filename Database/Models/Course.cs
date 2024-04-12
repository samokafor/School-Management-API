using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolManagementAPI.Database.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string CourseCode { get; set; }
        public int CreditHours { get; set; }
        public int? LecturerStaffId { get; set; }
        public List<string>? LecturerName { get;set; }
        [ForeignKey("LecturerStaffId")]
        public List<Staff>? Lecturer { get; set; }
        public int? DepartmentId { get; set; }
        [JsonIgnore]
        public Department? Department { get; set; }

    }
}
