using Prototypes.Model.Interfaces;
using Prototypes.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace Prototypes.Model;

public class RehearsalsViewModel : ObservableObject
{
    private DateTime _minDate = DateTime.Now;
    private TimeSpan _minTime = DateTime.Now.TimeOfDay;
    private DateTime _minRehearsalTime;
    private DateTime _maxDate = DateTime.Now.AddYears(1);
    private TimeSpan _maxTime = DateTime.Now.TimeOfDay;
    private DateTime _maxRehearsalTime;
    public DateTime MinRehearsalTime
    {
        get { return _minRehearsalTime; }
        set
        {
            SetProperty(ref _minRehearsalTime, value);
            FilterRehearsalsByDate();
        }
    }
    public DateTime MinDate
    {
        get { return _minDate; }
        set
        {
            SetProperty(ref _minDate, value);
            MinRehearsalTime = MinDate + MinTime;
        }
    }
    public TimeSpan MinTime
    {
        get { return _minTime; }
        set
        {
            SetProperty(ref _minTime, value);
            MinRehearsalTime = MinDate + MinTime;
        }
    }
    public DateTime MaxRehearsalTime
    {
        get { return _maxRehearsalTime; }
        set
        {
            SetProperty(ref _maxRehearsalTime, value);
            FilterRehearsalsByDate();
        }
    }
    public DateTime MaxDate
    {
        get { return _maxDate; }
        set
        {
            SetProperty(ref _maxDate, value);
            MaxRehearsalTime = MaxDate + MaxTime;
        }
    }
    public TimeSpan MaxTime
    {
        get { return _maxTime; }
        set
        {
            SetProperty(ref _maxTime, value);
            MaxRehearsalTime = MaxDate + MaxTime;
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

        MinRehearsalTime = DateTime.Now;
        MaxRehearsalTime = DateTime.Now.AddYears(1);

        FilteredRehearsals = Rehearsals;
    }

    private void FilterRehearsalsByDate()
    {
        FilteredRehearsals = new ObservableCollection<Rehearsal>();
        var matchingRehearsals = Rehearsals.Where(rehearsal => rehearsal.Time >= MinRehearsalTime && rehearsal.Time <= MaxRehearsalTime).ToList();
        foreach(Rehearsal rehearsal in matchingRehearsals)
        {
            FilteredRehearsals.Add(rehearsal);
        }
            
        Console.WriteLine("dummy");
    }
}
