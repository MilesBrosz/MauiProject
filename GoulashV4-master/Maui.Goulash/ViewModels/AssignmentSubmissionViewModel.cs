using System;
using Library.Goulash.Models;

using Library.Goulash.Services;

namespace MAUI.Goulash.ViewModels
{
	public class AssignmentSubmissionViewModel
	{
		Submission submission { get; set; }

        /*public AssignmentSubmissionViewModel(int id=0)
		{
			//search for the assignment by ID, assign attributes of this assignment to submission,
			//fill out the rest of the members for submission with bindingContext on this page
			//i can search throuch the lambdas and find the course and the assignment and add it to a variable
			//course = CourseService.Current.Courses.Where(c => c.Id == 1) as Course;
			course = CourseService.Current.GetById(1) as Course;
			assignment = course.Assignments.Where(a => a.Id == id) as Assignment; //this should be selected assignment
			submission = new Submission();
		}*/


        public AssignmentSubmissionViewModel(Assignment assignment)
        {
			course = CourseService.Current.Courses.Where(c => c.Assignments.Any(a => a == assignment)) as Course;
            //assignment = course.Assignments.Where(a => a.Id == id) as Assignment; //this should be selected assignment
            submission = new Submission();
        }

        public Course course { get; set; }
		public Assignment assignment { get; set; }
		public string Content { get; set; }

		public void SubmitClick()
		{
            //Assignment = GetByid(Id); Assignment.Submssion = Submission; Shell.Gotoasync("//StudentCourseDetail?studentId={1}");
            submission.Content = Content;
			submission.Assignment = assignment;
			submission.Student = StudentService.Current.Students.Where(s => s.Id == 1) as Student;
			Shell.Current.GoToAsync($"//StudentCourseDetail?studentId={1}");
		}
	}
}

