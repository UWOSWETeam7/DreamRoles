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
        var label = (Label)sender;
        var song = (Song)label.BindingContext;
        int userId = _performer.Id;
        String songName = song.Title;
        
        String answer = MauiProgram.BusinessLogic.AddSongForPerformer(userId, songName);
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