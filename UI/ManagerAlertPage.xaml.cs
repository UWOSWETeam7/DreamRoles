using Prototypes.Model;
using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class ManagerAlertPage : ContentPage
{
    private Performer _performer;
    /// <summary>
    /// ManagerAlertPage 
    /// </summary>
    /// <param name="currentPerformer"></param>
    public ManagerAlertPage(Performer currentPerformer)
    {
        InitializeComponent();
        _performer = currentPerformer;
        lblPerformerName.Text = currentPerformer.FirstName + " " + currentPerformer.LastName;
        //Might be able to pull performer's title later
        lblTitle.Text = "Performer";
        lblAbsences.Text = currentPerformer.Absences.ToString();
        ObservableCollection<Rehearsal> rehearsals = MauiProgram.BusinessLogic.GetPerformerRehearsals(currentPerformer);
        int i = 0;
        Boolean found = false;
        while (i < rehearsals.Count - 1 && found == false)
        {

            if (rehearsals[i].Song.Title == rehearsals[i + 1].Song.Title)
            {
                found = true;
            } else
            {
                i++; 
            }
           
        }
        if (found == true) {
            lblSongMissed.Text = rehearsals[i].Song.Title;
            lblDate1.Text = rehearsals[i].Time.ToString("MM/dd/yyyy");
            lblDate2.Text = rehearsals[i + 1].Time.ToString("MM/dd/yyyy");
            lblTime1.Text = rehearsals[i].Time.ToString("t");
            lblTime2.Text = rehearsals[i + 1].Time.ToString("t");
        }

    }

    private void ShowManagerPerformerInfoPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerPerformerInfoPage(_performer));
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}