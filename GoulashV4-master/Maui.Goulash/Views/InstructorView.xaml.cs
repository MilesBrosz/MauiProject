using MAUI.Goulash.ViewModels;

namespace MAUI.Goulash.Views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
		InitializeComponent();
		BindingContext = new InstructorViewViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ResetView();
        (BindingContext as InstructorViewViewModel).RefreshView();
    }

    private void AddEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddEnrollmentClick(Shell.Current);
    }
    private void RemoveEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemoveEnrollmentClick();
    }

    private void EditEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).EditEnrollmentClick(Shell.Current);
    }

    private void AddCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).AddCourseClick(Shell.Current);
    }

    private void RemoveCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).RemoveCourseClick();
    }
    private void EditCourseClick(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).EditCourseClick(Shell.Current);
    }

    private void Toolbar_EnrollmentsClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ShowEnrollments();
    }

    private void Toolbar_CoursesClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewViewModel).ShowCourses();
    }

}