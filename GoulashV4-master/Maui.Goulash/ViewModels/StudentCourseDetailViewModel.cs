using Library.Goulash.Models;
using Library.Goulash.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUI.Goulash.ViewModels
{
	public class StudentCourseDetailViewModel : INotifyPropertyChanged
	{
        public StudentCourseDetailViewModel(int id = 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }

            IsEnrollmentDetailsVisible = true;
            IsModuleDetailsVisible = false;
            IsAssignmentDetailsVisible = false;
        }

        public bool IsEnrollmentDetailsVisible { get; set; }
        public bool IsModuleDetailsVisible { get; set; }
        public bool IsAssignmentDetailsVisible { get; set; }

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

        public string Name { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public int Id { get; set; }

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

        public Assignment SelectedAssignment { get; set; }

        public void EnterSubmissionClick(Shell s)
        {
            //var idParam = SelectedAssignment?.Id ?? 0;
            Assignment assignment = SelectedAssignment;
            //s.GoToAsync($"//AssignmentSubmission?assignmentId={idParam}");
            s.GoToAsync($"//AssignmentSubmission?assignmentId={assignment}");
        }

        public void BackClick(Shell s)
        {
           // Shell.Current.GoToAsync($"//StudentCourseDetail?studentId={1}");
        }
    }
}

