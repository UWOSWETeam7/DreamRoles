using Prototypes.Databases;
using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keith Thoong
public partial class AddSongToSetlistPage : ContentPage
{
   private Performer _performer;
   private Song _song;
    public AddSongToSetlistPage(Song song)
    {
        InitializeComponent();
        _song = song;
        BindingContext = new SearchBarPerformerViewModel();

        //show performer names in the dropdown
        //once user selects their name, navigate to PerformerHomePage.xaml for that specific performer
    }
    private void OnNextClicked(object sender, EventArgs e)
    {
        if (_performer == null)
        {
            DisplayAlert(null, "You must select a performer whose setlist you will be adding this song to", "Okay");
            return;
        }

        String answer = MauiProgram.BusinessLogic.AddSongForPerformer(_performer.Id, _song.Title);
        if (answer == null)
        {
            DisplayAlert(null, $"Successfully added {_song.Title} to {_performer.FirstName}'s setlist", "Okay");
            Navigation.PopAsync();
        }

    }

    private void CVPerformers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _performer = CVPerformers.SelectedItem as Performer;
    }

}