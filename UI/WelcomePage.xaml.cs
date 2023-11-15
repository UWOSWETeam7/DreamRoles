using CommunityToolkit.Maui.Core.Views;
using Prototypes.Databases;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class WelcomePage : ContentPage
{
    int accessCode = 0;
    int performerAccessCode = 1122333;
    int stageManagerAccessCode = 4455666;
    int choreographerAccessCode = 7788999;
    public WelcomePage()
    {
        InitializeComponent();
        Access.Text = accessCode.ToString();
    }

    private async void OnEnter_Clicked(object sender, EventArgs e)
    {
        //if the performer access code is entered, go to select name page and show names in dropdown- SelectNamePage.xaml
        //if stage manager access code is entered, go to stage manager home page- ManagerHomePage.xaml
        //if choreographer access code is entered, go to choreographer home page- ChoreoHomePage.xaml

        var enteredCode = int.Parse(access.Text);
        if (enteredCode == performerAccessCode )
        {
            await Navigation.PushAsync(new SelectNamePage());
        } else if (enteredCode == stageManagerAccessCode )
        {
            await Navigation.PushAsync(new ManagerHomePage());
        } else if (enteredCode == choreographerAccessCode )
        {
            await Navigation.PushAsync(new ChoreoHomePage());
        } else 
        {
            //Todo: Some sort invalid message
        }
        Access.Text = accessCode.ToString();
    }

  
}