using Library.Goulash.Models;
using Library.Goulash.Services;
using MAUI.Goulash.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAUI.Goulash.Views;

[QueryProperty(nameof(PersonId), "personId")]
public partial class StudentCourseView : ContentPage
{
    public StudentCourseView()
    {
        InitializeComponent();
        BindingContext = new PersonDetailViewModel();
    }

    public int PersonId { get; set; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        PersonId = 1; //this is potentially a timing issue, show it works otherwise
        BindingContext = new StudentCourseViewViewModel(PersonId);
    }

    void ViewCourseClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentCourseViewViewModel).ViewCourseDetailsClick(Shell.Current);
    }

    void BackClicked(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync("//Student");
    }
}
