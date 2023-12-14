
namespace Prototypes.UI;

public partial class AddSongPopup : ContentPage
{
	public AddSongPopup()
	{
		InitializeComponent();
	}

    private void AddSong(object sender, EventArgs e)
    {
        //Get song from the entry
        string songName = songNameEntry.Text;

        //Check that the user entered something in the entry
        if(songName == null )
        {
            DisplayAlert("Error", "Please enter a song title.", "Ok");
        }
        else
        {
            //Ask the BusinessLogic to add a song
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