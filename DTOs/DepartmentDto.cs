namespace SchoolManagementAPI.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } // Ensure this property exists
        public string DepartmentCode { get; set; } // Ensure this property exists
        public int FacultyID { get; set; }
    }
}
