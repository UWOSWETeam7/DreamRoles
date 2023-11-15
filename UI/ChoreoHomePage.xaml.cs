using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ChoreoHomePage : ContentPage
{
    public ChoreoHomePage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();
    }
    private void ShowSongListPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerSongsPage());
    }

    public async void OnChoreoMenu_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChoreoMenuPage());
    }
}