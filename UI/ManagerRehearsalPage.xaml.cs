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

    private void ShowSongListPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerSongsPage());
    }
}