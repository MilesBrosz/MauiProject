using System;
using Library.Goulash.Models;
using ServerLibrary.GoulashAPI.Database;

namespace ServerLibrary.GoulashAPI.EC
{
	public class StudentEC
	{
        private static StudentEC? instance;

        public static StudentEC Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new StudentEC();
                }
                return instance;
            }
        }

        private StudentEC()
		{
		}

		public IEnumerable<Student> GetStudents()
		{
			return FakeDatabase.Students.Take(100);
		}

        public Student? GetStudentById(int Id)
        {
            return FakeDatabase.Students.FirstOrDefault(x => x.Id == Id);
        }

        public Student? AddOrUpdate(Student student)
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
            FakeDatabase.Students.Add(student);

            var returnVal = FakeDatabase.Students.FirstOrDefault(c => c.Id == student.Id);
            if (returnVal != null)
            {
                return returnVal; 
            }
            return null;
        }

        public Student? Delete(int Id)
        {
            var StudentToDelete = FakeDatabase.Students.FirstOrDefault(x => x.Id == Id);
            FakeDatabase.Students.Remove(StudentToDelete);

            return StudentToDelete;
        }
    }
}
