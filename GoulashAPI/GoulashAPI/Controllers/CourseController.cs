using System;
using Library.Goulash.Models;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.GoulashAPI.EC;
using ServerLibrary.GoulashAPI.Models;

namespace GoulashAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]

	public class CourseController : ControllerBase
	{
		private readonly ILogger<CourseController> _logger;

		public CourseController(ILogger<CourseController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{Id}")] //get a single course by Id

		public Course? Get(int Id)
		{
			return CourseEC.Current.GetCourseById(Id);
		}

		[HttpGet] //get the course list

		public IEnumerable<Course> GetCourses()
		{
			return CourseEC.Current.GetCourses();
		}

		[HttpPost] 
		public Course? AddOrUpdate([FromBody] Course course)
		{
			return CourseEC.Current.AddOrUpdate(course);
		}

		[HttpDelete("Delete/{Id}")]

		public Course? Delete(int Id)
		{
			return CourseEC.Current.Delete(Id);
		}
	}
}

