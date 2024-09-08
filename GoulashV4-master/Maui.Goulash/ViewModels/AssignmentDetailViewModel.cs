using System;
using Library.Goulash.Services;
using Library.Goulash.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MAUI.Goulash.ViewModels
{
    public class AssignmentDetailViewModel : INotifyPropertyChanged
    {
        public AssignmentDetailViewModel(int id = 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }
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
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public DateTime AssignmentDueDate {get;set;} //date time
        public Decimal AssignmentTotalAvailablePoints { get; set; } //decimal

        public void AddAssignment(int Id)
        {
            CourseService.Current.GetById(Id).Assignments.Add(new Assignment { Name = AssignmentName, Description = AssignmentDescription });
        }
    }
}

