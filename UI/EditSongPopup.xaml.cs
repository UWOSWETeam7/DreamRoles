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
        string songName = songNameEntry.Text;
        string artistName = artistNameEntry.Text;
        int duration = Int32.Parse(durationEntry.Text);
        int setlistId = 0;
            setlistId = _song.SetlistId;
        
        string oldSongName = _song.Title;
        string oldArtist = _song.Artist;
        MauiProgram.StageManagerBL.EditSong(setlistId, oldSongName, oldArtist, songName, artistName, duration);
        Navigation.PopAsync();
    }
}