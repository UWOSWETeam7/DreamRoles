using Prototypes.Model;
using System.Reflection;

namespace Prototypes.UI;
//author: Kaia Thern
public partial class ChoreoNotCheckedInPage : ContentPage
{
    public ChoreoNotCheckedInPage(Rehearsal rehearsal)
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

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}