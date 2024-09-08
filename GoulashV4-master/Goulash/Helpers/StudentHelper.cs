using System.Configuration.Assemblies;
using System.Diagnostics.Contracts;
using Goulash.Models;
using Goulash.Services;
namespace Goulash.Helpers {
    internal class StudentHelper {
        private StudentService studentService;
        private CourseService courseService;
        public StudentHelper() {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }
        public void CreateStudent(Student? selectedStudent = null) {
            Console.WriteLine("What is the Name of the Student?");
            var name = Console.ReadLine();
            Console.WriteLine("What is the ID of the Student?");
            var id = Console.ReadLine();
            Console.WriteLine("What is the Classification of the Student {(F)reshman, S(o)phomore, (J)unior, (S)enior}");
            var classification = Console.ReadLine() ?? string.Empty;
            StudentClassifiction classEnum = StudentClassifiction.Freshman;
            if(classification.Equals("O", StringComparison.InvariantCultureIgnoreCase)) {
                classEnum = StudentClassifiction.Sophomore;
            }
            else if(classification.Equals("J", StringComparison.InvariantCultureIgnoreCase)) {
                classEnum = StudentClassifiction.Junior;
            }
            else if(classification.Equals("S", StringComparison.InvariantCultureIgnoreCase)) {
                classEnum = StudentClassifiction.Senior;
            }

            bool isCreated =false;
            if(selectedStudent == null) {
                isCreated = true;
                selectedStudent = new Student{Name = name?? string.Empty, Classification = classEnum, Id = int.Parse(id ?? "0") };
            }
            selectedStudent.Id = int.Parse(id ?? "0");
            selectedStudent.Name = name ?? string.Empty;
            selectedStudent.Classification = classEnum;
            
            if(isCreated) {
               studentService.Add(selectedStudent); 
            }
        }

        public void ListStudents() {
            studentService.Students.ForEach(Console.WriteLine);

            Console.WriteLine("Select a student:");
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");

            Console.WriteLine("Student Course list:");
            courseService.Courses.Where(c=>c.Roster.Any(s=>s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void SearchStudents() {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);
            var selection = Console.ReadLine();
            var selectionInt = int.Parse(selection ?? "0");
            
            Console.WriteLine("Student Course list:");
            courseService.Courses.Where(c=>c.Roster.Any(s=>s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

        public void UpdateStudent() {
            Console.WriteLine("Select a Student to update");
            //ListStudents();
            studentService.Students.ForEach(Console.WriteLine);
            var query = Console.ReadLine();
            
            if(int.TryParse(query, out int selection)) {
                var selectedStudent = studentService.Students.FirstOrDefault(s=> s.Id == selection);
                if(selectedStudent != null) {
                    CreateStudent(selectedStudent);
                }
            }
        }
    }
}