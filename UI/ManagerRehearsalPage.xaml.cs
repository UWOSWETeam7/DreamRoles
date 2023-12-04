using Prototypes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace Prototypes.UI;

public partial class ManagerRehearsalPage : ContentPage
{
	public ManagerRehearsalPage(Rehearsal rehearsal)
	{
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel(rehearsal);
    }


    private void ShowAddPerformerPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPerformerPopup());
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this performer?", "Yes", "Cancel");

        if (userResponse)
        {
            MauiProgram.BusinessLogic.DeletePerformer(performer.Id);
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

    public async void OnManagerMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManagerMenuPage());
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