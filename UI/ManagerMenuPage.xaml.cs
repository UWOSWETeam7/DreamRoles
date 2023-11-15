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
}