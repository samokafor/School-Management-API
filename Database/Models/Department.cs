using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementAPI.Database.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public int HeadOfDepartmentStaffId { get; set; }

         // Exclude from database schema
        public string HeadOfDepartment { get; set; } // Denormalized field for HOD name

        [ForeignKey("HeadOfDepartmentStaffId")]
        public Staff HOD { get; set; }
        public List<Staff> StaffMembers { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
