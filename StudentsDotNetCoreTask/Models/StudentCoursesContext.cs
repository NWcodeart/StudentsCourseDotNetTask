using Microsoft.EntityFrameworkCore;

namespace StudentsDotNetCoreTask.Models
{
    public class StudentCoursesContext : DbContext
    {
        public StudentCoursesContext(DbContextOptions<StudentCoursesContext> options):base(options) { }

        public DbSet<Students> Students { get; set; }
        public DbSet<Courses> Courses { get; set; }
    }
}
