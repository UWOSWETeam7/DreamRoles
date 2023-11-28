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

        MauiProgram.BusinessLogic.EditSong(oldSongName, newSongName);
        Navigation.PopAsync();
    }
}