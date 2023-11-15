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


    private ObservableCollection<ISongDB> _songs;
    public ObservableCollection<ISongDB> Songs
    {
        get { return _songs; }
        set { SetProperty(ref _songs, value); }
    }

    private ObservableCollection<ISongDB> _filteredSongs;
    public ObservableCollection<ISongDB> FilteredSongs
    {
        get { return _filteredSongs; }
        set { SetProperty(ref _filteredSongs, value); }
    }

    public SearchBarPerformersSongsViewModel(Performer performer)
    {
        Songs = performer.Songs;
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
            FilteredSongs = new ObservableCollection<ISongDB>(
               Songs.Where(Song => Song.Title.ToLower().Contains(SearchText.ToLower())));
        }
    }
}
