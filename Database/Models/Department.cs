using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementAPI.Database.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public int? HeadOfDepartmentStaffId { get; set; }

        public string? HeadOfDepartment { get; set; }

        [ForeignKey("HeadOfDepartmentStaffId")]
        public Staff? HOD { get; set; }
        public List<Staff> StaffMembers { get; set; }
        public List<Course> Courses { get; set; }
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
