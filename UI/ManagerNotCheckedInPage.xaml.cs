using Prototypes.Model;
using System.Reflection;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class ManagerNotCheckedInPage : ContentPage
{
    public ManagerNotCheckedInPage()
    {
        InitializeComponent();
        try
        {
            BindingContext = new SearchBarNotCheckedInPerformerViewModel();
           
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
}