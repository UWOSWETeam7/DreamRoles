using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoCheckedInPage : ContentPage
{
	public ChoreoCheckedInPage(Rehearsal rehearsal)
	{
        InitializeComponent();
        BindingContext = new SearchBarCheckedInPerformerViewModel(rehearsal);
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}