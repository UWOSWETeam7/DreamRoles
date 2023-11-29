
namespace Prototypes.UI;

public partial class AddSongPopup : ContentPage
{
	public AddSongPopup()
	{
		InitializeComponent();
	}

    private void AddSong(object sender, EventArgs e)
    {
        string songName = songNameEntry.Text;
        if(songName == null )
        {
            DisplayAlert("Error", "Please enter a song title.", "Ok");
        }
        else
        {
            String answer = MauiProgram.BusinessLogic.AddSong(songName);
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