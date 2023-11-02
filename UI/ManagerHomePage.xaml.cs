using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class ManagerHomePage : ContentPage
{
    private RefreshView _refreshView = new RefreshView();
    public ManagerHomePage()
    {
        InitializeComponent();
        BindingContext = MauiProgram.StageManagerBL;
        _refreshView.Refreshing += ShowManagerPerformerInfoPage;
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
            MauiProgram.StageManagerBL.DeletePerformer(performer.Id);
        }
    }

    private void ShowManagerPerformerInfoPage(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var performer = (Performer) label.BindingContext;
        Navigation.PushAsync(new ManagerPerformerInfoPage(performer));
        //_refreshView.IsRefreshing = true;
    }

    private void ShowSongListPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SongsPage());
    }
}