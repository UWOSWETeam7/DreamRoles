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
        Navigation.PopAsync();
    }
}