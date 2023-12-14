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
        //Get the song title from the entry
        string newSongName = songNameEntry.Text;
        //Get the old song title from the song
        string oldSongName = _song.Title;

        //Make sure the user entered something into entry
        if (newSongName == null)
        {
            DisplayAlert("Error", "Please enter a song title.", "Ok");
        }

        else
        {
            //Ask the BusinessLogic to edit the song's title
            String answer = MauiProgram.BusinessLogic.EditSong(oldSongName, newSongName); 

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