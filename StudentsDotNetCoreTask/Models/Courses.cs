using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsDotNetCoreTask.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string TeacherName { get; set; }

        public ICollection<Students> students { get; set; }
    }
}
