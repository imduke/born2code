using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc4WebApplication1.Controllers
{
    public class CoursesController : ApiController
    {
        private static List<Course> courses = InitCourses();

        private static List<Course> InitCourses()
        {
            var ret = new List<Course>();
            ret.Add(new Course() { Id = 1, Title = "Title 1" });
            ret.Add(new Course() { Id = 2, Title = "Title 2" });

            return ret;
        }

        public IEnumerable<Course> Get()
        {
            return courses;
        }

    }

    public class Course
    {
        public int Id {get; set;}
        public string Title {get; set;}
    }
}
