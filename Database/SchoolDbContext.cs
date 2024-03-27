using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database.Models;
using static SchoolManagementAPI.Database.Models.Staff;

namespace SchoolManagementAPI.Database
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.HOD)
                .WithMany()
                .HasForeignKey(d => d.HeadOfDepartmentStaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Department)
                .WithMany(d => d.StaffMembers)
                .HasForeignKey(s => s.DepartmentId);


            modelBuilder.Entity<Staff>()
                .Property(s => s.Gender)
                .HasConversion(
                    v => v.ToString(), // Convert enum to string for storage
                    v => (Genders)Enum.Parse(typeof(Genders), v)); // Convert string to enum when reading from the database

            modelBuilder.Entity<Staff>()
                .Property(s => s.Title)
                .HasConversion(
                v => v.ToString(),
                    v => (Titles)Enum.Parse(typeof(Titles), v));
            
            modelBuilder.Entity<Staff>()
                .Property(s => s.StaffGrade)
                .HasConversion(
                v => v.ToString(),
                    v => (StaffGrades)Enum.Parse(typeof(StaffGrades), v));
        }
    }
}
