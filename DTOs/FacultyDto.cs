using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.DTOs
{
    public class FacultyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // Ensure this property exists
        public string FacultyCode { get; set; } // Ensure this property exists
        public List<DepartmentDto> Departments { get; set; }
    }
}
