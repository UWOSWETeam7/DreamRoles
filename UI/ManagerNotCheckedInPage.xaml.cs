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
            BindingContext = MauiProgram.StageManagerBL;
        } catch (TargetInvocationException tie)
        {
            throw; 
        }
    }
    /// <summary>
    /// ShowAlert_Clicked navigates to ManagerAlertPage from ManagerNotCheckedInPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    async void ShowAlert_Clicked(System.Object sender, System.EventArgs e)
    {
        Performer currentPerformer = CV.SelectedItem as Performer;
        await Navigation.PushModalAsync(new ManagerAlertPage(currentPerformer)); 
    }
}