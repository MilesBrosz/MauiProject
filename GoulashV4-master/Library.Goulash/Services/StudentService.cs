using Library.Goulash.Database;
using Library.Goulash.Models;
using Library.Goulash.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Library.Goulash.Services
{
    public class StudentService
    {

        private static StudentService? _instance;

        public List<Student> Students;
        /*{
            get 
            {
                return FakeDatabase.People.Where(p => p is Student).Select(p => p as Student);
            }
        }*/

        private StudentService()
        {
            var response = new WebRequestHandler()
                .Get("/Student").Result;

            Students = JsonConvert
                .DeserializeObject<List<Student>>(response)
                ?? new List<Student>();
        }

        public static StudentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentService();
                }

                return _instance;
            }
        }

        public void Add(Student student)
        {
            new WebRequestHandler().Post("/Student", student);
            Students.Add(student);

            //FakeDatabase.People.Add(student);
        }

        /*public void Update(Student student)
        {
            var response = new WebRequestHandler().Post("/Student", student).Result;
            var studentFromService = JsonConvert.DeserializeObject<Student>(response);
            if (studentFromService != null)
            {
                Students.Add(studentFromService);
            }
            else
            {
                if (studentFromService == null)
                    return;
                var existingStudent = Students.FirstOrDefault(c => c.Id == studentFromService.Id);
                var theIndex = Students.IndexOf(existingStudent);
                Students.Remove(existingStudent);
                Students.Insert(theIndex, studentFromService);
            }
        }*/

        public void Remove(Student student)
        {
            var id = student.Id;
            new WebRequestHandler().Delete($"/Student/Delete/{id}");
            Students.Remove(student);
            FakeDatabase.People.Remove(student);
        }

        public Student? GetById(int id)
        {
            var response = new WebRequestHandler()
                .Get($"/Student/{id}").Result;
            return JsonConvert.DeserializeObject<Student>(response);
            //return FakeDatabase.People.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Student?> Search(string query)
        {
            return Students.Where(s => (s != null) && s.Name.ToUpper().Contains(query.ToUpper()));
        }
    }
}
