//using Android.Telecom;
using Library.Goulash.Models;
using Library.Goulash.Services;
using Library.Goulash.Utilities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//using static Android.Graphics.ImageDecoder;


namespace MAUI.Goulash.ViewModels
{
    class CourseDetailViewModel : INotifyPropertyChanged
    {
        public CourseDetailViewModel(int id = 0)
        {
            if (id > 0)
            {
                LoadById(id);
                IsOldCourse = true;
            }
            else
                IsOldCourse = false;

            IsEnrollmentDetailsVisible = true;
            IsModuleDetailsVisible = false;
            IsAssignmentDetailsVisible = false;
        }

        public bool IsEnrollmentDetailsVisible { get; set; }
        public bool IsModuleDetailsVisible { get; set; }
        public bool IsAssignmentDetailsVisible { get; set; }
        public bool IsOldCourse { get; set; }

        public void ShowEnrollmentDetails()
        {
            IsEnrollmentDetailsVisible = true;
            IsModuleDetailsVisible = false;
            IsAssignmentDetailsVisible = false;
            NotifyPropertyChanged("IsEnrollmentDetailsVisible");
            NotifyPropertyChanged("IsModuleDetailsVisible");
            NotifyPropertyChanged("IsAssignmentDetailsVisible");
        }

        public void ShowModuleDetails()
        {
            IsEnrollmentDetailsVisible = false;
            IsModuleDetailsVisible = true;
            IsAssignmentDetailsVisible = false;
            NotifyPropertyChanged("IsEnrollmentDetailsVisible");
            NotifyPropertyChanged("IsModuleDetailsVisible");
            NotifyPropertyChanged("IsAssignmentDetailsVisible");
        }

        public void ShowAssignmentDetails()
        {
            IsEnrollmentDetailsVisible = false;
            IsModuleDetailsVisible = false;
            IsAssignmentDetailsVisible = true;
            NotifyPropertyChanged("IsEnrollmentDetailsVisible");
            NotifyPropertyChanged("IsModuleDetailsVisible");
            NotifyPropertyChanged("IsAssignmentDetailsVisible");
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var course = CourseService.Current.GetById(id) as Course;
            if (course != null)
            {
                Name = course.Name;
                Id = course.Id;
                Description = course.Description;
                Prefix = course.Prefix;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Id));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(Prefix));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Person> Students
        {
            get 
            {    
                return new ObservableCollection<Person>(StudentService.Current.Students); 
            }
        }

        public ObservableCollection<Person> Roster
        {
            get
            {
                var course = CourseService.Current.GetById(Id) as Course; //why is this null?
                if (course == null)
                    return null;
                return new ObservableCollection<Person>(course.Roster); //this meaning the course attached to this page
            }
        }

        public Student SelectedStudent {get;set;}

        public string Name { get; set;}
        public string Description { get; set;}
        public string Prefix { get; set;}
        public int Id { get; set; }

        public void AddCourse()
        {
            if (Id <= 0)
            {
                CourseService.Current.Add(new Course {Name = Name, Description = Description, Prefix = Prefix });
            }
            else 
            {
                var refToUpdate = CourseService.Current.GetById(Id) as Course;
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.Prefix = Prefix;

            }
            Shell.Current.GoToAsync("//Instructor");
        }

        public void AddToRoster()
        {   
            (CourseService.Current.GetById(Id) as Course).Roster.Add(SelectedStudent); //needs to be after the course was already made
            NotifyPropertyChanged(nameof(Roster));
            NotifyPropertyChanged(nameof(SelectedStudent));
        }

        public Student SelectedRemove { get; set;}

        public void RemoveFromRoster()
        {
            //same deal, but act on roster list
            (CourseService.Current.GetById(Id) as Course).Roster.Remove(SelectedRemove);
            NotifyPropertyChanged(nameof(Roster));
            NotifyPropertyChanged(nameof(SelectedRemove));
        }

        public Module SelectedModule { get; set; }

        public ObservableCollection<Module> Modules
        {
            get
            {
                var course = CourseService.Current.GetById(Id) as Course;
                if (course == null)
                    return null;
                return new ObservableCollection<Module>(course.Modules);
            }
        }

        public void AddModuleClick(Shell s)
        {
            var idparam = Id; //id of the current course
            s.GoToAsync($"//ModuleDetail?courseId={idparam}");
        }

        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                var course = CourseService.Current.GetById(Id) as Course;
                if (course == null)
                    return null;
                return new ObservableCollection<Assignment>(course.Assignments);
            }
        }

        public void AddAssignmentClick(Shell s)
        {
            var idparam = Id;
            s.GoToAsync($"//AssignmentDetail?courseId={idparam}");
        }
    }
}
