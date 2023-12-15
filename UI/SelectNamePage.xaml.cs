using Prototypes.Databases;
using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class SelectNamePage : ContentPage
{
    Performer performer;
    Database db = new Database();
    public SelectNamePage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();

        //show performer names in the dropdown
        //once user selects their name, navigate to PerformerHomePage.xaml for that specific performer
    }
    private void OnNextClicked(object sender, EventArgs e)
    {
        if (performer != null)
        {
            db.CheckInPerformer(performer, "checked in");
            Navigation.PushAsync(new PerformerCheckInTimesPage(performer));
            Navigation.RemovePage(this);
            //change checked in status of this performer
        }
    }

    private void CVPerformers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        performer = CVPerformers.SelectedItem as Performer;
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