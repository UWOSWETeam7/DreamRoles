using Prototypes.Model.Interfaces;
using Prototypes.UI;
using System.Collections.ObjectModel;

namespace Prototypes.Model;

    public class SearchBarNotCheckedInPerformerViewModel : ObservableObject 
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


    private ObservableCollection<Performer> _notCheckedInPerformers;
    public ObservableCollection<Performer> NotCheckedInPerformers
    {
        get { return _notCheckedInPerformers; }
        set { SetProperty(ref _notCheckedInPerformers, value); }
    }

    private ObservableCollection<Performer> _filteredNotCheckedInPerformers;
    public ObservableCollection<Performer> FilteredNotCheckedInPerformers
    {
        get { return _filteredNotCheckedInPerformers; }
        set { SetProperty(ref _filteredNotCheckedInPerformers, value); }
    }

    public SearchBarNotCheckedInPerformerViewModel()
    {
        NotCheckedInPerformers = MauiProgram.BusinessLogic.GetNotCheckedInPerformers;
        FilteredNotCheckedInPerformers = NotCheckedInPerformers;
    }

    private void FilterNames()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            FilteredNotCheckedInPerformers = NotCheckedInPerformers;
        }
        else
        {
            var searchWords = SearchText.Trim().Split(' ');

            FilteredNotCheckedInPerformers = new ObservableCollection<Performer>(
             NotCheckedInPerformers.Where(performer =>
                 searchWords.All(word =>
                     performer.FirstName.ToLower().Contains(word.ToLower()) ||
                     performer.LastName.ToLower().Contains(word.ToLower())
                 )
             )
         );
        }
    }

}


