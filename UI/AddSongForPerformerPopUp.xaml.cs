using Prototypes.Model;


namespace Prototypes.UI;

public partial class AddSongForPerformerPopUp : ContentPage
{
    private Performer _performer;
	public AddSongForPerformerPopUp(Performer performer)
	{
        _performer = performer;
		InitializeComponent();
        BindingContext = new SearchBarSongViewModel();
    }

    private void AddSong(object sender, EventArgs e)
    {
        //Get the song  that the user clicked on
        var label = (Label)sender;
        var song = (Song)label.BindingContext;
        //Get the song's title
        String songName = song.Title;

        //Get the user id from the performer you are adding a song to
        int userId = _performer.Id;
        

        //Change to what it needs to be
        String answer = MauiProgram.BusinessLogic.AddSongForPerformer(userId, songName, null);

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