namespace Prototypes.UI;

public partial class ChoreoMenuPage : ContentPage
{
	public ChoreoMenuPage()
	{
		InitializeComponent();
	}
    public async void OnSongs_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoSongsPage());
    }
    public async void OnNotCheckedIn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoNotCheckedInPage());
    }
    public async void OnCheckedIn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoCheckedInPage());
    }
    public async void OnRehearsals_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoRehearsalsPage());
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }

}