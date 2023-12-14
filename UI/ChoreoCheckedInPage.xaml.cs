using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoCheckedInPage : ContentPage
{
	public ChoreoCheckedInPage(Rehearsal rehearsal)
	{
        InitializeComponent();
        BindingContext = new SearchBarCheckedInPerformerViewModel(rehearsal);
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