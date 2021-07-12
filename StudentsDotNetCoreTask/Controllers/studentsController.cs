using Microsoft.AspNetCore.Mvc;
using StudentsDotNetCoreTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDotNetCoreTask.Controllers
{
    public class studentsController : Controller
    {
        private readonly StudentCoursesContext _db;
        private int _studentId;

        public studentsController(StudentCoursesContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
          
                //var course = new Courses {  CourseName = "no course", TeacherName = "none", students = null };
                //_db.Courses.Add(course);
                //_db.SaveChanges();
            
            var result = new List<Students>();
           
                result = _db.Students.Select(s => new Students
                {
                    Id = s.Id,
                    Name = s.Name,
                    courses = _db.Courses.ToList()
                }).ToList();
            

            return View(result);
        }
        //Get
        public IActionResult AddOrEdit(int id = 0)
        {

            return View(new Students());
        }


        public IActionResult StudentCourses(int id = 0)
        {
            _studentId = id;
            var result = new List<StudentsInfo>();
            using (var db = _db)
            {
                result = db.Students.Where(s => s.Id == id).Select(x => new StudentsInfo
                {
                    id = x.Id,
                    Name = x.Name,
                    courseInfo = x.courses.Select(y => new CourseInfo
                    {
                        CourseName = y.CourseName,
                        TeacherName = y.TeacherName
                    }).ToList()
                }).ToList();
            }

            

            return View(result);
        }
        //public List<Courses> UnAddedCourses( List<Courses> AddedCourses)
        //{
        //    var unAddedCourses = new List<Courses>();
        //    var allCourse = _db.Courses.ToList();

        //    foreach ( var x in allCourse)
        //    {
        //        if (x.Equals(AddedCourses))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            unAddedCourses.Add(x);
        //        }
                
        //    }

        //    return unAddedCourses;
        //}
        public IActionResult AddNewCourseToStudentView( int studentId)
        {
            //var SCoursesUnAdded = new List<Students>();
            //using (var db = _db)
            //{
            //    SCoursesUnAdded = db.Students.Where(s => s.Id == id).Select(x => new Students
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        courses = UnAddedCourses(x.courses.ToList()) //this function will return unadded courses
            //    }).ToList();
            //}
            ViewBag.StudentId = studentId;

            var courses = new List<CoursesList>();

            courses =_db.Courses.Select(c => new CoursesList
            {
                CourseId = c.Id,
                CourseName = c.CourseName
            }).ToList();


            return PartialView("PlaceHolderHare", courses);
        }
        public void AddNewCourseToStudent(int courseId , int studentId)
        {
            var courseAdd = new List<Courses>();
            using(_db)
            {
                Students student = new Students { Id = studentId };
                _db.Students.Add(student);
                _db.Students.Attach(student);
                Courses course = new Courses { Id = courseId };
                _db.Courses.Add(course);
                _db.Courses.Attach(course);
                try
                {
                    student.courses = courseAdd;
                    courseAdd.Add(course);
                    _db.SaveChanges();

                }catch(Exception)
                {
                    ViewBag.ErrorMassege = "this course is added";
                }

            }


            //var course = _db.Courses.Where(c => c.Id == courseId);
            //Students StudentsCourses = _db.Students.Where(s => s.Id == studentId);

            //    _db.Students.Update((Students)StudentsCourses);
            //    _db.SaveChanges();

            //_db.Students.Where(s => s.Id == _studentId).Select(s => new Students
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    courses = 
            //}) ;

        }
    }
}
    