using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class PerformersOfSongPage : ContentPage
{
    private Song _song;
    public PerformersOfSongPage(Song song)
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformersOfSongsViewModel(song);
        _song = song;
    }

    private void AddToSetlist_Clicked(object sender, EventArgs e)
    {
        ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, IsEnabled = true };
        Navigation.PushAsync(new AddSongToSetlistPage(_song));
    }
}