//using Android.Net;
using Library.Goulash.Models;
using Library.Goulash.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using static Android.Resource;
//using static Java.Util.Jar.Attributes;
//using static Android.Provider.Contacts;
//using static Android.Resource;

namespace MAUI.Goulash.ViewModels
{
    public class StudentCourseViewViewModel : INotifyPropertyChanged
    {
        Student student;

        public StudentCourseViewViewModel(int id = 0)
        {
            if(id>0)
            {
                LoadById(id);
            }
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                var filteredList = CourseService
                    .Current
                    .Courses
                    .Where(Course => Course.Roster.Any(Student => Student.Id == Id));

                return new ObservableCollection<Course>(filteredList);
            }
        }

        public Course SelectedCourse { get; set; }
        public int Id {get;set;} //id of the current student

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var person = StudentService.Current.GetById(id) as Student;
            if (person != null)
            {
                Id = person.Id;
                student = person;
            }
            //student = person;
            NotifyPropertyChanged(nameof(Courses));
        }

        public void ViewCourseDetailsClick(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//StudentCourseDetail?courseId={idParam}");
        }
    }
}

