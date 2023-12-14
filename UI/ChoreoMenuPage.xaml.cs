using Prototypes.Model;
namespace Prototypes.UI;

public partial class ChoreoMenuPage : ContentPage
{
    private Rehearsal _rehearsal;
	public ChoreoMenuPage(Rehearsal rehearsal)
	{
        _rehearsal = rehearsal;
		InitializeComponent();
	}
    public async void OnSongs_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoSongsPage());
    }
    public async void OnNotCheckedIn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoNotCheckedInPage(_rehearsal));
    }
    public async void OnCheckedIn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoCheckedInPage(_rehearsal));
    }
    public async void OnRehearsals_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoRehearsalsPage());
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