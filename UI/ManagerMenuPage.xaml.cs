namespace Prototypes.UI;

public partial class ManagerMenuPage : ContentPage
{
	public ManagerMenuPage()
	{
		InitializeComponent();
	}

	public async void OnSongs_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManagerSongsPage());
	}
    public async void OnNotCheckedIn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ManagerNotCheckedInPage());
    }
    public async void OnCheckedIn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ManagerCheckedInPage()); 
    }

    public async void OnAccessCodes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerAccesssCodePage());
    }
    public async void OnRehearsals_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerRehearsalsPage());
    }
}