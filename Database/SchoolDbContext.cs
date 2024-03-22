using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Database.Models;

namespace SchoolManagementAPI.Database
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }
        public DbSet<Faculty> Faculties { get; set; }
    }
}
