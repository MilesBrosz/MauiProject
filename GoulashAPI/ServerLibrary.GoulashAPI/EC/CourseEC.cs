using System;
using Library.Goulash.Models;
using ServerLibrary.GoulashAPI.Database;
using ServerLibrary.GoulashAPI.Models;

namespace ServerLibrary.GoulashAPI.EC
{
	public class CourseEC
	{
		private static CourseEC? instance;

		public static CourseEC Current
		{
			get
			{
				if(instance == null)
				{
					instance = new CourseEC();
				}
				return instance;
			}
		}

		private CourseEC ()
		{

		}

		public IEnumerable<Course> GetCourses()
		{
			return FakeDatabase.Courses.Take(100); //add current if want to use the singleton

			/*foreach(var c in returnList)
			{
				c.Student = new CourseEC().Get(c.CourseId);
			}
			return returnList;
			*/
		}

		public Course? GetCourseById(int Id)
		{
			return FakeDatabase.Courses.FirstOrDefault(x => x.Id == Id);
		}

		public Course? AddOrUpdate(Course course)
		{
			/*if (course.Id <=0)
			{
                //course.Id = FakeDatabase.LastCourseId + 1; //private setter
                //FakeDatabase.Courses.Add(new Course(course));
                FakeDatabase.Courses.Add(course);
            }
			else
			{
				var CourseToUpdate = FakeDatabase.Courses.FirstOrDefault(x => x.Id == course.Id);
				if(CourseToUpdate != null)
				{
					FakeDatabase.Courses.Remove(CourseToUpdate);
					FakeDatabase.Courses.Add(course);
				}
            }*/

			//focus on add first
			FakeDatabase.Courses.Add(course);

			var returnVal = FakeDatabase.Courses.FirstOrDefault(c => c.Id == course.Id);
			if(returnVal != null)
			{
				return returnVal; //Course(returnVal);
			}
			return null;
		}

		public Course? Delete(int Id)
		{
			var CourseToDelete = FakeDatabase.Courses.FirstOrDefault(x => x.Id == Id);
			FakeDatabase.Courses.Remove(CourseToDelete);

			return CourseToDelete;
        }
    }
}

