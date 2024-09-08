using System;
using System.Text;
using Goulash.Models;
using Goulash.Services;
namespace Goulash.Helpers {
    public class CourseHelper {
        private CourseService courseService;
        private StudentService studentService; //singletons in order to manage one instance of the lists, and the same services can communicate with each other aka the lists
        
        public CourseHelper() {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }
        public void CreateCourse(Course? selectedCourse = null) {
            Console.WriteLine("What is the name of the course?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course?");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which Student should be Enrolled in this Course? ('Q' to Quit)");
            var roster = new List<Student>();
            bool continueAdd =true;
            
            while(continueAdd) {
                studentService.Students.Where(s=> !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine); //show all students that could be added, but not duplicates of those who are already in the list
                var selection = "Q";
                if(studentService.Students.Any(s => !roster.Any(s2 => s2.Id == s.Id))) {
                    selection = Console.ReadLine() ?? string.Empty; //override selection
                }

                if(selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase)) {
                    continueAdd = false;
                }
                else {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);

                    if(selectedStudent != null) {
                        roster.Add(selectedStudent);
                    }
                }
            }
            
            Console.WriteLine("Would you like to add Assignments (Y/N)?");
            var response = Console.ReadLine() ?? "N";
            var assignments = new List<Assignment>();

            if(response.Equals("Y", StringComparison.InvariantCultureIgnoreCase)) {
                continueAdd = true;
                while(continueAdd) {
                    Console.WriteLine("Name:");
                    var assName = Console.ReadLine();

                    Console.WriteLine("Description:");
                    var assDescription = Console.ReadLine() ?? string.Empty;
                    
                    Console.WriteLine("Total Points:");
                    var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");

                    Console.WriteLine("Due Date:");
                    var dueDate = DateTime.Parse(Console.ReadLine() ?? "04/20/420");
        
                    assignments.Add(new Assignment {Name = assName, Description = assDescription, TotalAvailablePoints = totalPoints, DueDate = dueDate});
                
                    Console.WriteLine("Add more Courses (Y/N)?");
                    response = Console.ReadLine() ?? "N";
                    if(response.Equals("N",StringComparison.InvariantCultureIgnoreCase)) {
                        continueAdd = false;
                    }
                }
            }

            bool isCreated = false;
            if(selectedCourse == null) {
                isCreated = true;
                selectedCourse = new Course();
            }
            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;
            selectedCourse.Roster = new List<Student>(); 
            selectedCourse.Roster.AddRange(roster);
            selectedCourse.Assignments = new List<Assignment>();
            selectedCourse.Assignments.AddRange(assignments);

            if(isCreated) {
                courseService.Add(selectedCourse);
            }
        }

        public void ListCourses() {
            courseService.Courses.ForEach(Console.WriteLine);
        }

        public void UpdateCourse() {
            Console.WriteLine("Select a Course to update");
            ListCourses();
            
            var query = Console.ReadLine();
            var selectedCourse = courseService.Courses.FirstOrDefault(s=> s.Code.Equals(query, StringComparison.InvariantCultureIgnoreCase));
            if(selectedCourse != null) {
                CreateCourse(selectedCourse);
            }
        }
        public void SearchCourses() { //by name or description
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            courseService.Search(query).ToList().ForEach(Console.WriteLine);
        }
    }
}