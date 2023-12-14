using Prototypes.Model.Interfaces;
using Prototypes.UI;
using System.Collections.ObjectModel;

namespace Prototypes.Model;

    public class SearchBarCheckedInPerformerViewModel : ObservableObject
    {
    private string _searchText;
    public string SearchText
    {
        get { return _searchText; }
        set
        {
            SetProperty(ref _searchText, value);
            FilterNames();
        }
    }


    private ObservableCollection<Performer> _checkedInPerformers;
    public ObservableCollection<Performer> CheckedInPerformers
    {
        get { return _checkedInPerformers; }
        set { SetProperty(ref _checkedInPerformers, value); }
    }

    private ObservableCollection<Performer> _filteredCheckedInPerformers;
    public ObservableCollection<Performer> FilteredCheckedInPerformers
    {
        get { return _filteredCheckedInPerformers; }
        set { SetProperty(ref _filteredCheckedInPerformers, value); }
    }

    public SearchBarCheckedInPerformerViewModel(Rehearsal rehearsal)
    {
        CheckedInPerformers = MauiProgram.BusinessLogic.GetCheckedInPerformers(rehearsal);
        FilteredCheckedInPerformers = CheckedInPerformers;
    }

    private void FilterNames()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            FilteredCheckedInPerformers = CheckedInPerformers;
        }
        else
        {
            var searchWords = SearchText.Trim().Split(' ');

            FilteredCheckedInPerformers = new ObservableCollection<Performer>(
             CheckedInPerformers.Where(performer =>
                 searchWords.All(word =>
                     performer.FirstName.ToLower().Contains(word.ToLower()) ||
                     performer.LastName.ToLower().Contains(word.ToLower())
                 )
             )
         );
        }
    }
}

