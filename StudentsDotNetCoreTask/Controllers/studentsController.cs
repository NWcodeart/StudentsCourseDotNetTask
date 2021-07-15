using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public static int  StudentId;

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
            StudentId = id;
            var StudentSelected = SelectStudent(StudentId);

            return View(StudentSelected);
        }
        public List<StudentsInfo> SelectStudent(int id)
        {
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
            return result;
        }
        public List<CoursesList> UnAddedCourses()
        {
            var studentInfo = _db.Students.Include(x=>x.courses).FirstOrDefault(s => s.Id == StudentId);
            var studentCourses = studentInfo.courses;


            var unAddedCourses = new List<CoursesList>();
            var allCourse = _db.Courses.ToList();

            foreach (var course in allCourse)
            {
                if (studentCourses.Any(x=>x.Id ==course.Id))
                {
                    continue;
                }
                else
                {
                    unAddedCourses.Add(new CoursesList 
                    {
                        CourseId = course.Id,
                        CourseName = course.CourseName
                    });
                }

            }

            return unAddedCourses;
        }

        public IActionResult AddNewCourseToStudentView(int courseId = -1  )
        {

            //if(courseId != -1)
            //{
            //    AddNewCourseToStudent(courseId);
            //    return StudentCourses();
            //}
            //else
            //{
            var unAddedCourses = UnAddedCourses();


                return PartialView("AddNewCourseToStudentView", unAddedCourses);
            //}
        }
        [HttpPost]
        public void AddNewCourseToStudent(int courseId  )
        {
            int studentId = StudentId;
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
    