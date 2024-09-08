using Library.Goulash.Database;
using Library.Goulash.Models;
using Library.Goulash.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Goulash.Services
{
    public class CourseService
    {
        private static CourseService? _instance;

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }

                return _instance;
            }
        }

        private CourseService()
        {
            var response = new WebRequestHandler()
                .Get("/Course").Result;

            Courses = JsonConvert
                .DeserializeObject<List<Course>>(response)
                ?? new List<Course>();
        }

        public Course? GetById(int id)
        {
            //return FakeDatabase.Courses.FirstOrDefault(p => p.Id == id);
            var response = new WebRequestHandler()
                .Get($"/Course/{id}").Result;
            return JsonConvert.DeserializeObject<Course>(response);
            
        }

        public void Add(Course course) //orUpdate
        {
            FakeDatabase.Courses.Add(course);
            new WebRequestHandler().Post("/Course", course);
            Courses.Add(course);
        }

        /*public void Update(Course course)
        {
            var response = new WebRequestHandler().Post("/Course", course).Result;
            var courseFromService = JsonConvert.DeserializeObject<Course>(response);
            if (courseFromService != null)
            {
                Courses.Add(courseFromService);
            }
            else
            //{
                if (courseFromService == null)
                    return;
                var existingCourse = Courses.FirstOrDefault(c => c.Id == courseFromService.Id);
                var theIndex = Courses.IndexOf(existingCourse);
                Courses.Remove(existingCourse);
                Courses.Insert(theIndex, courseFromService);
            //}
            //Add(course);
            //var existingCourse = Courses.FirstOrDefault(c => c.Id == course.Id);
            //var theIndex = Courses.IndexOf(course);
            //Courses.Remove(course);
            //Courses.Insert(theIndex, course);

        }*/

        public void Remove(Course course)
        {

            FakeDatabase.Courses.Remove(course);
            //replace all calls to this service to calls to the WRH, to connect server and this client side code
            var id = course.Id;
            new WebRequestHandler().Delete($"/Course/Delete/{id}");
            Courses.Remove(course);
            
        }

        public List<Course> Courses;
        /*{
            get
            {
                return FakeDatabase.Courses;
            }
        }*/

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }

        public string GetLetterGrade(decimal grade)
        {
            if (grade >= 93)
            {
                return "A";
            }
            else if (grade < 93 && grade >= 90)
            {
                return "A-";
            }
            else if (grade < 90 && grade >= 87)
            {
                return "B+";
            }
            else if (grade < 87 && grade >= 83)
            {
                return "B";
            }
            else if (grade < 83 && grade >= 80)
            {
                return "B-";
            }
            else if (grade < 80 && grade >= 77)
            {
                return "C+";
            }
            else if (grade < 77 && grade >= 73)
            {
                return "C";
            }
            else if (grade < 73 && grade >= 70)
            {
                return "C-";
            }
            else if (grade < 70 && grade >= 60)
            {
                return "D";
            }
            else
            {
                return "F";
            }

        }

        public decimal GetGradePoints(decimal grade)
        {
            if (grade >= 93)
            {
                return 4M;
            }
            else if (grade < 93 && grade >= 90)
            {
                return 3.7M;
            }
            else if (grade < 90 && grade >= 87)
            {
                return 3.3M;
            }
            else if (grade < 87 && grade >= 83)
            {
                return 3M;
            }
            else if (grade < 83 && grade >= 80)
            {
                return 2.7M;
            }
            else if (grade < 80 && grade >= 77)
            {
                return 2.3M;
            }
            else if (grade < 77 && grade >= 73)
            {
                return 2M;
            }
            else if (grade < 73 && grade >= 70)
            {
                return 1.7M;
            }
            else if (grade < 70 && grade >= 60)
            {
                return 1M;
            }
            else
            {
                return 0M;
            }
        }

    }
}
