using Prototypes.Model;

namespace Prototypes.UI;

public partial class AddSongForPerformerPopUp : ContentPage
{
    private Performer _performer;
	public AddSongForPerformerPopUp(Performer performer)
	{
        _performer = performer;
		InitializeComponent();
	}

    private void AddSong(object sender, EventArgs e)
    {
        int userId = _performer.Id;
        string songName = songNameEntry.Text;
        string artistName = artistNameEntry.Text;
        int duration = int.Parse(durationEntry.Text);
        MauiProgram.BusinessLogic.AddSongForPerformer(userId, songName);
        Navigation.PopAsync();
    }
}