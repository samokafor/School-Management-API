using SchoolManagementAPI.Database.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementAPI.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public int HeadOfDepartmentStaffId { get; set; }
        public string HeadOfDepartment { get; set; }
        //public Staff HOD {  get; set; }
        public List<Staff> StaffMembers { get; set; }
        public int FacultyID { get; set; }
    }
}
