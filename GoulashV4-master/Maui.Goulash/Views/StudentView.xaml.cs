using MAUI.Goulash.ViewModels;

namespace MAUI.Goulash.Views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
		BindingContext = new StudentViewViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    void ViewDetailsClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as StudentViewViewModel).DetailsClick(Shell.Current);
    }
}