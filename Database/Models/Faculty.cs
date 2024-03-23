using System.Text.Json.Serialization;

namespace SchoolManagementAPI.Database.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FacultyCode { get; set; }
       
        public List<Department>? Departments { get; set; }
    }
}
