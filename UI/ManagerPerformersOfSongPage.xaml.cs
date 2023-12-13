using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ManagerPerformersOfSongPage : ContentPage
{
    private Song _song;
    public ManagerPerformersOfSongPage(Song song)
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformersOfSongsViewModel(song);
        _song = song;
        title.Text = "Performers Of" + " " + _song.Title ;
        
    }

    private void AddToSetlist_Clicked(object sender, EventArgs e)
    {
        ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, IsEnabled = true };
        Navigation.PushAsync(new AddSongToSetlistPage(_song));
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}