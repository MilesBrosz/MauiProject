using System;
using System.Xml.XPath;
using Goulash.Helpers;
using Goulash.Models;
using Goulash.Services;

namespace Goulash {
    internal class Program {
        static void Main(string[] args) {
            
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();

            bool cont = true;

            while(cont) {
                printMenu();
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result)) {
                    if(result == 1)
                        studentHelper.CreateStudent();
                    else if(result == 2)
                        studentHelper.ListStudents();
                    else if(result == 3)
                        studentHelper.SearchStudents();
                    else if(result == 4)
                        studentHelper.UpdateStudent();
                    else if(result == 5)
                        courseHelper.CreateCourse();
                    else if(result == 6)
                        courseHelper.ListCourses();
                    else if(result == 7)
                        courseHelper.UpdateCourse();
                    else if(result == 8)
                        courseHelper.SearchCourses();
                    else
                        cont = false;
                        break;
                }
            }

        }
        static void printMenu() { //create an assignment and add it to course assignments
            Console.WriteLine("1. Add a Student"); //2
            Console.WriteLine("2. List all Enrolled Students"); //7 and 9
            Console.WriteLine("3. Search for Student"); //8
            Console.WriteLine("4. Update a Student"); //11 and 12
            Console.WriteLine("5. Add a Course"); //1 and 3
            Console.WriteLine("6. List all Courses"); //5
            Console.WriteLine("7. Update a Course"); //10, and 4, remove student from a course is implicitely included in here
            Console.WriteLine("8. Search for a Course"); //6
            Console.WriteLine("{Other Number}. Exit"); //12
        }
    }
}