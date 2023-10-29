namespace Prototypes.UI;

public partial class AddSongForPerformerPopUp : ContentPage
{
	public AddSongForPerformerPopUp()
	{
		InitializeComponent();
	}

    private void AddSong(object sender, EventArgs e)
    {
        int userId = 1223;
        string songName = songNameEntry.Text;
        string artistName = artistNameEntry.Text;
        string duration = durationEntry.Text;
        MauiProgram.StageManagerBL.AddSongForPerformer(userId, songName, artistName, duration);
    }
}