using System;
using Library.Goulash.Models;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.GoulashAPI.EC;

namespace GoulashAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StudentController : ControllerBase
	{
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> Logger)
        {
            _logger = Logger;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return StudentEC.Current.GetStudents();
        }

        [HttpGet("{Id}")]
        public Student? Get(int Id)
        {
            return StudentEC.Current.GetStudentById(Id);
        }

        [HttpPost]
        public Student? AddOrUpdate([FromBody] Student student)
        {
            return StudentEC.Current.AddOrUpdate(student);
        }

        [HttpDelete("Delete/{Id}")]

        public Student? Delete(int Id)
        {
            return StudentEC.Current.Delete(Id);
        }
    }
}

