using Prototypes.Model;
namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class CheckedInPage : ContentPage
{
    public CheckedInPage()
    {
        InitializeComponent();
        BindingContext = MauiProgram.StageManagerBL;
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this performer?", "Yes", "Cancel");

        if (userResponse)
        {
            MauiProgram.StageManagerBL.DeletePerformer(performer.Id);
        }
    }
}