using CommunityToolkit.Maui.Core.Views;
using Prototypes.Databases;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class WelcomePage : ContentPage
{
   
    public WelcomePage()
    {
        InitializeComponent();
    }

    private async void OnEnter_Clicked(object sender, EventArgs e)
    {
        int performerAccessCode = Int32.Parse(MauiProgram.BusinessLogic.GetPerformerAccessCode());
        int stageManagerAccessCode = Int32.Parse(MauiProgram.BusinessLogic.GetManagerAccessCode());
        int choreographerAccessCode = Int32.Parse(MauiProgram.BusinessLogic.GetChoreoAccessCode());
        //if the performer access code is entered, go to select name page and show names in dropdown- SelectNamePage.xaml
        //if stage manager access code is entered, go to stage manager home page- ManagerHomePage.xaml
        //if choreographer access code is entered, go to choreographer home page- ChoreoHomePage.xaml
        try
        {
            var enteredCode = int.Parse(access.Text);
            if (enteredCode == performerAccessCode)
            {
                await Navigation.PushAsync(new SelectNamePage());
                Navigation.RemovePage(this);
            }
            else if (enteredCode == stageManagerAccessCode)
            {
                await Navigation.PushAsync(new ManagerHomePage());
                Navigation.RemovePage(this);
            }
            else if (enteredCode == choreographerAccessCode)
            {
                await Navigation.PushAsync(new ChoreoHomePage());
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("Invalid Access Code", "This is a invalid access code. Please try a different code.", "Ok");
            }
        }
        catch(Exception ev)
        {
            await DisplayAlert("Warning", "Please enter a access code", "Ok");
        }
    }  
}