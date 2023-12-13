using Prototypes.Model;

namespace Prototypes.UI;
public partial class ChoreoRehearsalPage : ContentPage
{
    Rehearsal _rehearsal;
    SearchBarPerformerViewModel _searchBarPerformerViewModel;

    public ChoreoRehearsalPage(Rehearsal rehearsal)
    {
        InitializeComponent();
        _rehearsal = rehearsal;
        _searchBarPerformerViewModel = new SearchBarPerformerViewModel(rehearsal);
        BindingContext = _searchBarPerformerViewModel;
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}