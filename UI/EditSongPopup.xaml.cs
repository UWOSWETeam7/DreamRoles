using Prototypes.Model;

namespace Prototypes.UI;

public partial class EditSongPopup : ContentPage
{
    private Song _song;
	public EditSongPopup(Song song)
	{
        
        InitializeComponent();
        _song = song;
    }
    private void EditSong(object sender, EventArgs e)
    {
        string newSongName = songNameEntry.Text;
        string oldSongName = _song.Title;
        string songName = songNameEntry.Text;
        if (newSongName == null)
        {
            DisplayAlert("Error", "Please enter a song title.", "Ok");
        }
        else
        {
            String answer = MauiProgram.BusinessLogic.EditSong(oldSongName, newSongName); ;
            if (answer == null)
            {
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", answer, "Ok");
            }
        }
    }
}