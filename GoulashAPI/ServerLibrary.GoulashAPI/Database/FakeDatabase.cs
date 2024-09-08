using System;
using Library.Goulash.Models;
using ServerLibrary.GoulashAPI.Models;

namespace ServerLibrary.GoulashAPI.Database
{
	public class FakeDatabase
	{
		private FakeDatabase()
		{
			//instance = null;
		}

		private static FakeDatabase? instance;

        public static FakeDatabase Current
		{
			get {
				if (instance == null)
					instance = new FakeDatabase();
				return instance;
			}
		}

		public static List<Course> Courses = new List<Course>();
        /*{
            new Course{Name="Course1" },
            new Course{Name="Course2" },
            new Course{Name="Course3" }
        };*/

        public static int LastCourseId
			=> Courses.Any()? Courses.Select(c => c.Id).Max() : 0;

		public static List<Student> Students = new List<Student>
		{
			new Student{ Name = "Student1"},
			new Student{ Name = "Student2"},
			new Student{ Name = "Student3"},
		};

        public static int LastStudentId
            => Students.Any() ? Students.Select(c => c.Id).Max() : 0;

    }
}

