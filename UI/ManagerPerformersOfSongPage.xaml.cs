using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ManagerPerformersOfSongPage : ContentPage
{
    private Song _song;
    private SearchBarPerformersOfSongsViewModel _viewModel;
    public ManagerPerformersOfSongPage(Song song)
    {
        InitializeComponent();
        _viewModel = new SearchBarPerformersOfSongsViewModel(song);
        BindingContext = _viewModel;
        _song = song;
        title.Text = "Performers Of" + " " + _song.Title ;
        
    }

    private void AddToSetlist_Clicked(object sender, EventArgs e)
    {
        ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, IsEnabled = true };
        Navigation.PushAsync(new AddSongToSetlistPage(_song));
    }

    private void RemoveFromSetlist_Clicked(object sender, EventArgs e)
    {
        Performer performer = CVPerformers.SelectedItem as Performer;

        if (performer == null)
        {
            DisplayAlert(null, "You must select a performer to remove the song from", "Okay");
            return;
        }

            if (MauiProgram.BusinessLogic.RemoveSongFromSetlist(performer, _song))
            {
                _viewModel.Performers = MauiProgram.BusinessLogic.GetPerformersOfASong(_song);
                _viewModel.FilteredPerformers = _viewModel.Performers;
            }
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());

        // Remove all pages from the navigation stack except the WelcomePage
        var existingPages = Navigation.NavigationStack.ToList();

        if (existingPages.Any())
        {
            foreach (var page in existingPages.Take(existingPages.Count - 1))
            {
                Navigation.RemovePage(page);
            }
        }
    }
}