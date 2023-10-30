namespace Prototypes.UI;
//@author: Kaia Thern
public partial class SongsPage : ContentPage
{
    public SongsPage()
    {
        InitializeComponent();
    }

    private async void DeleteSong(object sender, EventArgs e)
    {
        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this song?", "Yes", "Cancel");
        
        if (userResponse)
        {
            String song = "";
            String artist = "";
            MauiProgram.StageManagerBL.DeleteSong(song, artist);
        }
    }

    private void ShowAddSongPopUp(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddSongPopup());
    }
}