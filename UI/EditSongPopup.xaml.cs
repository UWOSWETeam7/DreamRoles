using Prototypes.Model;

namespace Prototypes.UI;

public partial class EditSongPopup : ContentPage
{
    private Song _song;
	public EditSongPopup(Song song)
	{
		Song _song = song as Song;
        InitializeComponent();
	}
    private void EditSong(object sender, EventArgs e)
    {
        string songName = songNameEntry.Text;
        string artistName = artistNameEntry.Text;
        int duration = Int32.Parse(durationEntry.Text);
        int setlistId = _song.SetlistId;
        string oldSongName = _song.Title;
        string oldArtist = _song.Artist;
        MauiProgram.StageManagerBL.EditSong(setlistId, oldSongName, oldArtist, songName, artistName, duration);
        Navigation.PopAsync();
    }
}