using System;
using Library.Goulash.Services;
using Library.Goulash.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MAUI.Goulash.ViewModels
{
	public class ModuleDetailViewModel : INotifyPropertyChanged
    {
		public ModuleDetailViewModel(int id=0)
		{
            if (id > 0)
            {
                LoadById(id);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name { get; set; }
		public string Description { get; set; }

        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }

		public int Id { get; set; }

        public void AddModule(int Id)
        {
            CourseService.Current.GetById(Id).Modules.Add(new Module { Name = ModuleName, Description= ModuleDescription});
            //NotifyPropertyChanged(nameof(Modules));
        }
    }
}

