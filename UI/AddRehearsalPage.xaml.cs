using Prototypes.Model;
namespace Prototypes.UI;

public partial class AddRehearsalPage : ContentPage
{
    public AddRehearsalPage()
    {
        InitializeComponent();
        CVSongs.ItemsSource = MauiProgram.BusinessLogic.Songs;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        
        Song song = CVSongs.SelectedItem as Song;
        DateTime rehearsalTime = DPDate.Date + TPTime.Time;

        var result = MauiProgram.BusinessLogic.AddRehersal(rehearsalTime, song.Title);

        String alertTitle = "Failed to Add Rehearsal";
        if (result.success)
        {
            alertTitle = "Success!";
        }

        DisplayAlert(alertTitle, result.message, "Okay");
        Navigation.PopAsync();
    }

}