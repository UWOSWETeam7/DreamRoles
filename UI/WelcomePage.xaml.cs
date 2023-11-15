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
        var enteredCode = int.Parse(access.Text);
        if (enteredCode == performerAccessCode )
        {
            await Navigation.PushAsync(new SelectNamePage("performer"));
        } else if (enteredCode == stageManagerAccessCode )
        {
            await Navigation.PushAsync(new SelectNamePage("stagemanager"));
        } else if (enteredCode == choreographerAccessCode )
        {
            await Navigation.PushAsync(new SelectNamePage("choreographer"));
        } else 
        {
            //Todo: Some sort invalid message
        }
        Access.Text = accessCode.ToString();
    }

    public void ShowPerformerNames(Boolean show)
    {
       //Database.SelectAllPerformers();
    }

    public void ShowStageManagerNames(Boolean show)
    {
        //Database.SelectAllStageManagers();
    }

    public void ShowChoreographerNames(Boolean show)
    {

    }
    private void OnEntryTextChanged(object sender, EventArgs e)
    {
        
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {

    }
}