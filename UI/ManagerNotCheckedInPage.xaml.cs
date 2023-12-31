using Prototypes.Model;
using System.Reflection;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class ManagerNotCheckedInPage : ContentPage
{
    public ManagerNotCheckedInPage(Rehearsal rehearsal)
    {
        InitializeComponent();
        try
        {
            BindingContext = new SearchBarNotCheckedInPerformerViewModel(rehearsal);
           
        }
        catch (TargetInvocationException tie)
        {
            throw;
        }
    }
    /// <summary>
    /// ShowAlert_Clicked navigates to ManagerAlertPage from ManagerNotCheckedInPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ShowAlert_Clicked(System.Object sender, System.EventArgs e)
    {
        var button = (Button)sender;
        var performer = (Performer)button.BindingContext;
        Navigation.PushAsync(new ManagerAlertPage(performer));
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