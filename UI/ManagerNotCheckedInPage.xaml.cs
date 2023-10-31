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
    //To-do make work
    void ShowAlert_Clicked(System.Object sender, System.EventArgs e)
    {

    }
}