namespace SchoolManagementAPI.Database.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        

    }
}
