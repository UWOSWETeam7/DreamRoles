namespace Prototypes.UI;

public partial class AddSongPopup : ContentPage
{
	public AddSongPopup()
	{
		InitializeComponent();
	}

    private void AddSong(object sender, EventArgs e)
    {
        int setlistId = Int32.Parse(setlistIdEntry.Text);
        string songName = songNameEntry.Text;
        string artistName = artistNameEntry.Text;
        int duration = Int32.Parse(durationEntry.Text);
        MauiProgram.BusinessLogic.AddSong(setlistId, songName, artistName, duration);
        Navigation.PopAsync();
    }
}