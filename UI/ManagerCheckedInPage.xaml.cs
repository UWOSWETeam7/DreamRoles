using Prototypes.Model;
namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class ManagerCheckedInPage : ContentPage
{
    public ManagerCheckedInPage(Rehearsal rehearsal)
    {
        InitializeComponent();
        BindingContext = new SearchBarCheckedInPerformerViewModel(rehearsal);
    }

    private async void DeletePerformer(object sender, EventArgs e)
    {
        var image = (Image)sender;
        var performer = (Performer)image.BindingContext;

        Boolean userResponse = await DisplayAlert("Confirmation", "Are you sure you want to delete this performer?", "Yes", "Cancel");

        if (userResponse)
        {
            MauiProgram.BusinessLogic.DeletePerformer(performer.Id);
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