using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ChoreoPerformersOfSongPage : ContentPage
{
    private Song _song;
    public ChoreoPerformersOfSongPage(Song song)
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformersOfSongsViewModel(song);
        _song = song;
        //Changes the title to the specific song choosen
        title.Text = $"{"Performers Of"} {_song.Title} ";
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