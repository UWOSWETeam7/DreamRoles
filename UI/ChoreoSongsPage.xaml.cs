using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoSongsPage : ContentPage
{
	public ChoreoSongsPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarSongViewModel();
    }


    private void ShowPerformersOfSongPage(object sender, EventArgs e)
    {
        //Get the song that was clicked on
        var button = (Button)sender;
        var song = (Song)button.BindingContext;
        Navigation.PushAsync(new ChoreoPerformersOfSongPage(song));

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