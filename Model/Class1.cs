using Prototypes.Model.Interfaces;
using Prototypes.UI;
using System.Collections.ObjectModel;

namespace Prototypes.Model;

public class RehearsalsViewModel : ObservableObject
{
    private string _searchText;
    public string SearchText
    {
        get { return _searchText; }
        set
        {
            SetProperty(ref _searchText, value);
            FilterRehearsalsByDate();
        }
    }


    private ObservableCollection<Rehearsal> _rehearsals;
    public ObservableCollection<Rehearsal> Rehearsals
    {
        get { return _rehearsals; }
        set { SetProperty(ref _rehearsals, value); }
    }

    private ObservableCollection<Rehearsal> _filteredRehearsals;
    public ObservableCollection<Rehearsal> FilteredRehearsals
    {
        get { return _filteredRehearsals; }
        set { SetProperty(ref _filteredRehearsals, value); }
    }

    public RehearsalsViewModel()
    {
        Rehearsals = MauiProgram.BusinessLogic.Rehearsals;
        FilteredRehearsals = Rehearsals;
    }

    private void FilterRehearsalsByDate()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            FilteredRehearsals = Rehearsals;
        }
        else
        {
            var searchWords = SearchText.Trim().Split(' ');
            /*
            FilteredPerformers = new ObservableCollection<Rehearsal>(
             Performers.Where(performer =>
                 searchWords.All(word =>
                     performer.FirstName.ToLower().Contains(word.ToLower()) ||
                     performer.LastName.ToLower().Contains(word.ToLower())
                 )
             )
         );
            */
        }
    }
}
