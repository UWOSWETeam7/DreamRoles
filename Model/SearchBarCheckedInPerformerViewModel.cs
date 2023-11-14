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


    private ObservableCollection<(Performer Performer, DateTime? CheckInTime)> _checkedInPerformers;
    public ObservableCollection<(Performer Performer, DateTime? CheckInTime)> CheckedInPerformers
    {
        get { return _checkedInPerformers; }
        set { SetProperty(ref _checkedInPerformers, value); }
    }

    private ObservableCollection<(Performer Performer, DateTime? CheckInTime)> _filteredCheckedInPerformers;
    public ObservableCollection<(Performer Performer, DateTime? CheckInTime)> FilteredCheckedInPerformers
    {
        get { return _filteredCheckedInPerformers; }
        set { SetProperty(ref _filteredCheckedInPerformers, value); }
    }

    public SearchBarCheckedInPerformerViewModel()
    {
        CheckedInPerformers = MauiProgram.BusinessLogic.GetCheckedInPerformers;
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

            FilteredCheckedInPerformers = new ObservableCollection<(Performer Performer, DateTime? CheckInTime)>(
             CheckedInPerformers.Where(performer =>
                 searchWords.All(word =>
                     performer.Performer.FirstName.ToLower().Contains(word.ToLower()) ||
                     performer.Performer.LastName.ToLower().Contains(word.ToLower())
                 )
             )
         );
        }
    }
}

