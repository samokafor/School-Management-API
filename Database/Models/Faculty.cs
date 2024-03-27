using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolManagementAPI.Database.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FacultyCode { get; set; }
        public int? DeanStaffId { get; set; } // Foreign key property
        
        public string? FacultyDean { get; set; }
        // Navigation property for the dean staff member
        [ForeignKey("DeanStaffId")]
        public Staff? Dean { get; set; }

        public List<Department> Departments { get; set; }
    }
}
