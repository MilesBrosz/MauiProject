using Library.Goulash.Models;
using MAUI.Goulash.ViewModels;
namespace MAUI.Goulash.Views;


[QueryProperty(nameof(CourseId), "courseId")]
public partial class AssignmentDetailView : ContentPage
{
	public AssignmentDetailView()
	{
		InitializeComponent();
		BindingContext = new AssignmentDetailViewModel(CourseId);
	}

	public int CourseId { get; set; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AssignmentDetailViewModel(CourseId);
    }

    void BackClick(System.Object sender, System.EventArgs e)
    {
        Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}"); //go back to the same edit page
    }

    void OkClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as AssignmentDetailViewModel).AddAssignment(CourseId); //should pass in course ID;
        Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}");
    }
}
