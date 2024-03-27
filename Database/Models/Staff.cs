using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolManagementAPI.Database.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public Titles Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Genders Gender {  get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StaffGrades StaffGrade { get; set; }
        public int? DepartmentId { get; set; }
        [JsonIgnore]
        //[ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public enum Titles
        {
            Mr,
            Mrs,
            Dr,
            Prof,
            Barr

        }

        public enum StaffGrades
        {
            AssistantLecturer,
            Lecturer1,
            Lecturer2,
            Lecturer3,
            SeniorLecturer,
            Professor,
            ProfessorEmeritus,
        }
        public enum Genders
        {
            Male,
            Female
        }
    }
}
