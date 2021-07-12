using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDotNetCoreTask.Models
{
    public class Students
    {
        [Key]
        [Required(ErrorMessage = "This field is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        public string Name { get; set; }
        public ICollection<Courses> courses { get; set; }

    }
}
