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

    private void OnEnterClicked(object sender, EventArgs e)
    {
        var enteredCode = int.Parse(access.Text);
        if (enteredCode == performerAccessCode )
        {

        } else if (enteredCode == stageManagerAccessCode )
        {

        } else if (enteredCode == choreographerAccessCode )
        {

        } else
        {
            
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