
using static SchoolManagementAPI.Database.Models.Staff;

namespace SchoolManagementAPI.DTOs
{
    public class StaffDto
    {
        public int Id { get; set; }
        public Titles Title { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public Genders Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public StaffGrades StaffGrade { get; set; }
        public int DepartmentId { get; set; }
    }
}
