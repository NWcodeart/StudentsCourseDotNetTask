using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDotNetCoreTask.Models
{
    public class StudentsInfo
    {
        public int id { get; set; }
        public string Name { get; set; }
        public List<CourseInfo> courseInfo { get; set; }
    }
}
