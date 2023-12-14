using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoCheckedInPage : ContentPage
{
	public ChoreoCheckedInPage(Rehearsal rehearsal)
	{
        InitializeComponent();
        BindingContext = new SearchBarCheckedInPerformerViewModel(rehearsal);
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this performer?", "Yes", "Cancel");

        if (userResponse)
        {
            MauiProgram.BusinessLogic.DeletePerformer(performer.Id);
        }
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}