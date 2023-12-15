using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class PerformerHomePage : ContentPage
{
    private Performer _performer;
    public PerformerHomePage(Performer performer)
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformersSongsViewModel(performer);
        _performer = performer;
        title.Text = _performer.FirstName + " " + performer.LastName + "'s Homepage";
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