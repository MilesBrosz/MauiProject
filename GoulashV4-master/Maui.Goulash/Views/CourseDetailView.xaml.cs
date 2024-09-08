using Library.Goulash.Models;
using Library.Goulash.Services;
using MAUI.Goulash.ViewModels;
using System.Collections.ObjectModel;

namespace MAUI.Goulash.Views;

[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDetailView : ContentPage
{
	public CourseDetailView()
	{
		InitializeComponent();
		BindingContext = new CourseDetailViewModel();
	}

    public int CourseId
    {
        set; get;
    }
    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse();
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailViewModel(CourseId);
    }

    private void AddToRosterClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddToRoster();
    }

    private void RemoveFromRosterClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RemoveFromRoster();
    }

    void Toolbar_EnrollmentDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).ShowEnrollmentDetails();
    }

    void Toolbar_ModuleDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).ShowModuleDetails();
    }

    void AddModuleClick(System.Object sender, System.EventArgs e)
    {
        //navigate to a module page
        (BindingContext as CourseDetailViewModel).AddModuleClick(Shell.Current);
    }

    void RemoveModuleClick(System.Object sender, System.EventArgs e)
    {
        //selected module, then remove from the listview, same with enrollments
    }

    void Toolbar_AssignmentDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).ShowAssignmentDetails();
    }

    void AddAssignmentClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddAssignmentClick(Shell.Current);
    }

    void RemoveAssignmentClick(System.Object sender, System.EventArgs e)
    {
    }
}