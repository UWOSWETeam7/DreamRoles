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
        Title = $"{"Performers Of"} {_song.Title} ";
    }
}