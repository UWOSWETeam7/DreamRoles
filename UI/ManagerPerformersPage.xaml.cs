using Prototypes.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ManagerPerformersPage : ContentPage
{

    private Rehearsal _nearestRehearsal;
    public ManagerPerformersPage()
    {
        InitializeComponent();

        BindingContext = new SearchBarPerformerViewModel();
    }


    private void ShowAddPerformerPopup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPerformerPopup());
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to remove this performer from this rehearsal?", "Yes", "Cancel");

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

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}