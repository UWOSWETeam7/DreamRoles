using Prototypes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ManagerHomePage : ContentPage
{

    private Rehearsal _nearestRehearsal;
    public ManagerHomePage()
    {
        InitializeComponent();

        // get the rehearsal closest to the current time by ording the list of rehearsals and selecting the first one that is on or further than the current time
        _nearestRehearsal = MauiProgram.BusinessLogic.Rehearsals.OrderBy(rehearsal => rehearsal.Time).First(rehearsal => rehearsal.Time >= DateTime.Now);
        LabelNearestRehearsal.Text = $"Next Rehearsal at {_nearestRehearsal.Time} for {_nearestRehearsal.Song.Title}";
        BindingContext = new SearchBarPerformerViewModel(_nearestRehearsal);
    }


    private void ShowAddPerformerPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPerformerToRehearsalPage(_nearestRehearsal));
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to remove this performer from this rehearsal?", "Yes", "Cancel");

        if (userResponse)
        {
            MauiProgram.BusinessLogic.RemovePerformerFromRehearsal(performer, _nearestRehearsal);
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
        var label = (Image) sender;
        var performer = (Performer)  label.BindingContext;
        string status = await DisplayActionSheet("Change performer check-in status to:",null, null, "checked in", "excused", "not checked in");

        MauiProgram.BusinessLogic.UpdatePerformerStatus(performer, status);


    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}