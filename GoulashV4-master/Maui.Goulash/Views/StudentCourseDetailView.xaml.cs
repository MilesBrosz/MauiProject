using Library.Goulash.Models;
using MAUI.Goulash.ViewModels;
namespace MAUI.Goulash.Views;

[QueryProperty(nameof(CourseId), "courseId")]
public partial class StudentCourseDetailView : ContentPage
{
    public StudentCourseDetailView()
    {
        InitializeComponent();
        BindingContext = new StudentCourseDetailViewModel();
    }

    public int StudentId { get; set; }
    public int CourseId
    {
        set; get;
    }

    void BackClick(System.Object sender, System.EventArgs e)
    {
		Shell.Current.GoToAsync($"//StudentCourse?studentId={1}");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        CourseId = 1; //becuase these quesry properties arent working
        BindingContext = new StudentCourseDetailViewModel(CourseId);
    }

    void Toolbar_EnrollmentDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowEnrollmentDetails();
    }

    void Toolbar_ModuleDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowModuleDetails();
    }

    void Toolbar_AssignmentDetailsClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowAssignmentDetails();
    }

    void EnterSubmissionClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).EnterSubmissionClick(Shell.Current);
        //add submission page, pass assignment ID 
    }
}
