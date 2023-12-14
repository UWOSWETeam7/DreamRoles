using Prototypes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Prototypes.UI;

public partial class ManagerRehearsalPage : ContentPage
{
    Rehearsal _rehearsal;
    SearchBarPerformerViewModel _searchBarPerformerViewModel;

    public ManagerRehearsalPage(Rehearsal rehearsal)
	{
        InitializeComponent();
        _rehearsal = rehearsal;
        _searchBarPerformerViewModel = new SearchBarPerformerViewModel(rehearsal);
        BindingContext = _searchBarPerformerViewModel;
    }


    private void ShowAddPerformerPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPerformerToRehearsalPage(_rehearsal));
        _searchBarPerformerViewModel.Performers = MauiProgram.BusinessLogic.GetPerformersOfARehearsal(_rehearsal);
    }

   

    private async void OnPerformerSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            Performer performer = (Performer)e.CurrentSelection[0];
            await Navigation.PushAsync(new ManagerPerformerInfoPage(performer));
            CVPerformers.SelectedItem = null;

        }
    }

    private async void ChangeCheckInStatus(object sender, EventArgs e)
    {
        var label = (Image)sender;
        var performer = (Performer)label.BindingContext;
        string status = await DisplayActionSheet("Change performer check-in status to:", null, null, "checked in", "excused", "not checked in");

        MauiProgram.BusinessLogic.UpdatePerformerStatus(performer, status);


    }

    private void ShowSongListPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerSongsPage());
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