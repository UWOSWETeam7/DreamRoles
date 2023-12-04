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
    }

    private async void RemovePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to remove this performer from this rehearsal?", "Yes", "Cancel");

        if (userResponse)
        {
            var invokeRemove = MauiProgram.BusinessLogic.RemovePerformerFromRehearsal(performer, _rehearsal);
            if (invokeRemove.success)
            {
                _searchBarPerformerViewModel.Performers.Remove(performer);
            }
        }
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

    private void ShowSongListPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerSongsPage());
    }
    private async void ChangeCheckInStatus(object sender, EventArgs e)
    {
        var label = (Image)sender;
        var performer = (Performer)label.BindingContext;
        string status = await DisplayActionSheet("Change performer check-in status to:", null, null, "checked in", "excused", "not checked in");

        MauiProgram.BusinessLogic.UpdatePerformerStatus(performer, status);


    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}