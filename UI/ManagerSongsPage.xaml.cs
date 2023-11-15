using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ManagerSongsPage : ContentPage
{
    public ManagerSongsPage()
    {
        InitializeComponent();
        BindingContext = new SearchBarSongViewModel();
    }

    private async void DeleteSong(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var song = (Song)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this song?", "Yes", "Cancel");
        
        if (userResponse)
        {
            MauiProgram.BusinessLogic.DeleteSong(song.Title, song.Artist);
        }
    }

    private void ShowAddSongPopUp(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddSongPopup());
    }

    private void ShowEditSongPopup(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var song = (Song)label.BindingContext;
        Navigation.PushAsync(new EditSongPopup(song));
    }
    private void ShowPerformersOfSongPage(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var song = (Song)button.BindingContext;
        Navigation.PushAsync(new PerformersOfSongPage(song));
    }
    
}