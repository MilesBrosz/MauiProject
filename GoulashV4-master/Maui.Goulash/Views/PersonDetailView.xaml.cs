using Library.Goulash.Models;
using Library.Goulash.Services;
using MAUI.Goulash.ViewModels;
using System.Collections.ObjectModel;

namespace MAUI.Goulash.Views;

[QueryProperty(nameof(PersonId), "personId")]
public partial class PersonDetailView : ContentPage
{

    public PersonDetailView()
    {
        InitializeComponent();
        BindingContext = new PersonDetailViewModel();
    }

    public int PersonId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
		(BindingContext as PersonDetailViewModel).AddPerson();
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
		BindingContext = null;
    }

	private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDetailViewModel(PersonId);
    }

}