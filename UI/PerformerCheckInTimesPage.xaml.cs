using CommunityToolkit.Maui.Views;
using Prototypes.Model;
using System.Security.Cryptography.X509Certificates;

namespace Prototypes.UI;
public partial class PerformerCheckInTimesPage : ContentPage
{
    private Performer _performer;
    private RefreshView _refreshView = new RefreshView();
    public PerformerCheckInTimesPage(Performer performer)
	{
        InitializeComponent();
        _performer = performer;
        CVRehearsals.ItemsSource = MauiProgram.BusinessLogic.GetPerformerRehearsals(performer);
        Title = $"{_performer.FirstName} {_performer.LastName} {"Check-In Times"}";
    }



    private async void OnRehearsalSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            Rehearsal rehearsal = (Rehearsal)e.CurrentSelection[0];
            await DisplayPopup(_performer, rehearsal);
        }
    }

    public async Task DisplayPopup(Performer performer, Rehearsal rehearsal)
    {
        bool result = (bool)await this.ShowPopupAsync(new RehearsalCheckInPopup(performer, rehearsal));
        if (result == true)
        {
            MauiProgram.BusinessLogic.UpdatePerformerRehearsalStatus(performer, rehearsal, true);
            await Navigation.PushAsync(new PerformerHomePage(_performer));
        }
        else
        {
            CVRehearsals.SelectedItem = null;
        }
    }
}