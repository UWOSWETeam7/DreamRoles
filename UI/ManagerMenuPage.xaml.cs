using Prototypes.Model;
namespace Prototypes.UI;

public partial class ManagerMenuPage : ContentPage
{
    private Rehearsal _rehearsal;
	public ManagerMenuPage(Rehearsal rehearsal)
	{
        _rehearsal = rehearsal;
		InitializeComponent();
	}

	public async void OnSongs_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ManagerSongsPage());
	}
    public async void OnNotCheckedIn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ManagerNotCheckedInPage(_rehearsal));
    }
    public async void OnCheckedIn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new ManagerCheckedInPage(_rehearsal)); 
    }

    public async void ManagerAdminPage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerAdminPage());
    }
    public async void OnRehearsals_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerRehearsalsPage());
    }
    public async void OnPerformers_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerPerformersPage());
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());

        // Remove all pages from the navigation stack except the WelcomePage
        var existingPages = Navigation.NavigationStack.ToList();

        if (existingPages.Any())
        {
            foreach (var page in existingPages.Take(existingPages.Count - 1))
            {
                Navigation.RemovePage(page);
            }
        }
    }
}