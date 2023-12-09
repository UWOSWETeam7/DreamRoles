using Prototypes.Model.Interfaces;
using Prototypes.UI;
using System.Collections.ObjectModel;

namespace Prototypes.Model;

public class SearchBarPerformersSongsViewModel : ObservableObject
{
    private string _searchText;
    public string SearchText
    {
        get { return _searchText; }
        set
        {
            SetProperty(ref _searchText, value);
            FilterTitles();
        }
    }


    private ObservableCollection<Song> _songs;
    public ObservableCollection<Song> Songs
    {
        get { return _songs; }
        set { SetProperty(ref _songs, value); }
    }

    private ObservableCollection<Song> _filteredSongs;
    public ObservableCollection<Song> FilteredSongs
    {
        get { return _filteredSongs; }
        set { SetProperty(ref _filteredSongs, value); }
    }

    public SearchBarPerformersSongsViewModel(Performer performer)
    {
        Songs = MauiProgram.BusinessLogic.GetPerfomrerSetlist(performer.Id);
        FilteredSongs = Songs;
    }

    private void FilterTitles()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            FilteredSongs = Songs;
        }
        else
        {
            FilteredSongs = new ObservableCollection<Song>(
               Songs.Where(Song => Song.Title.ToLower().Contains(SearchText.ToLower())));
        }
    }
}
