using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.Database
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId);
        }
    }
}
