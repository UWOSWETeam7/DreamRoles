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
}