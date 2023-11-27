using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoSongsPage : ContentPage
{
	public ChoreoSongsPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarSongViewModel();
    }
    private void ShowPerformersOfSongPage(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var song = (Song)button.BindingContext;
        Navigation.PushAsync(new PerformersOfSongPage(song));
    }
}