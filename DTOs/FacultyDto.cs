using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.DTOs
{
    public class FacultyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FacultyCode { get; set; }
        public int DeanStaffId { get; set; }
        public string FacultyDean { get; set; }
        public Staff Dean { get; set; }
        public List<DepartmentDto> Departments { get; set; }
    }
}
