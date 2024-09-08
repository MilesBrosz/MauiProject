using Library.Goulash.Models;
using MAUI.Goulash.ViewModels;

namespace MAUI.Goulash.Views;

//[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(assignment), "assignment")]
public partial class AssignmentSubmissionView : ContentPage
{
    public AssignmentSubmissionView()
    {
        InitializeComponent();
        //BindingContext = new AssignmentSubmissionViewModel();
    }

    public int AssignmentId {get;set;}
    public Assignment assignment { get; set; }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        //AssignmentId = 1; //for test
        //BindingContext = new AssignmentSubmissionViewModel(AssignmentId);
        BindingContext = new AssignmentSubmissionViewModel(assignment);
    }

    void SubmitClick(System.Object sender, System.EventArgs e)
    {
        (BindingContext as AssignmentSubmissionViewModel).SubmitClick();
    }
}
