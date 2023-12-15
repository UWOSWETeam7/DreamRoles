using Prototypes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ChoreoHomePage : ContentPage
{
    private SearchBarPerformerViewModel _viewModel;
    private Rehearsal _nearestRehearsal;
    public ChoreoHomePage()
    {
        InitializeComponent();

        // get the rehearsal closest to the current time by ording the list of rehearsals and selecting the first one that is on or further than the current time
        _nearestRehearsal = MauiProgram.BusinessLogic.Rehearsals.OrderBy(rehearsal => rehearsal.Time).FirstOrDefault(rehearsal => rehearsal.Time >= DateTime.Now);
        if (_nearestRehearsal == null)
        {
            _nearestRehearsal = MauiProgram.BusinessLogic.Rehearsals.First();
        }
        LabelNearestRehearsal.Text = $"Next Rehearsal at {_nearestRehearsal.Time} for {_nearestRehearsal.Song.Title}";
        _viewModel = new SearchBarPerformerViewModel(_nearestRehearsal);
        BindingContext = _viewModel;
    }

    private async void ChangeCheckInStatus(object sender, EventArgs e)
    {
        var label = (Image)sender;
        var performer = (Performer)label.BindingContext;
        string status = await DisplayActionSheet("Change performer check-in status to:", null, null, "checked in", "excused", "not checked in");

        MauiProgram.BusinessLogic.UpdatePerformerStatus(performer, status);
    }

    private async void OnChoreoMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoMenuPage(_nearestRehearsal));
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());

        // Remove all pages from the navigation stack except the WelcomePage
        var existingPages = Navigation.NavigationStack.ToList();

        if (existingPages.Any())
        {
            foreach (var page in existingPages.Take(existingPages.Count - 1))
            {
                Navigation.RemovePage(page);
            }
        }
    }
}