using CommunityToolkit.Maui.Views;
using Prototypes.Model;
using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class ManagerAlertPage : ContentPage
{
    private Performer _performer;
    private Song _song;
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
        try
        {
            ObservableCollection<Rehearsal> rehearsals = MauiProgram.BusinessLogic.GetPerformerMissedRehearsals(currentPerformer);
            int i = 0;
            Boolean found = false;
            while (i < rehearsals.Count - 1 && found == false)
            {

                if (rehearsals[i].Song.Title == rehearsals[i + 1].Song.Title)
                {
                    found = true;
                }
                else
                {
                    i++;
                }

            }
            if (found == true)
            {
                _song = rehearsals[i].Song;
                lblSongMissed.Text = rehearsals[i].Song.Title;
                lblDate1.Text = rehearsals[i].Time.ToString("MM/dd/yyyy");
                lblDate2.Text = rehearsals[i + 1].Time.ToString("MM/dd/yyyy");
                lblTime1.Text = rehearsals[i].Time.ToString("t");
                lblTime2.Text = rehearsals[i + 1].Time.ToString("t");
            }
        } catch (Exception ex)
        {
            DisplayAlert("Error:", ex.Message, "OK");
        }

    }
    /// <summary>
    /// Navigates to performer's contact information located in ManagerPerformerInfoPage
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowManagerPerformerInfoPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerPerformerInfoPage(_performer));
    }
    /// <summary>
    /// Signs out the stagemanager and goes back to welcome page to sign in a dream role user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Asks stagemanager whether they want to pull a song from the performer's setlist when checking in. If yes the song is pulled. If no the song stays in the setlist.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void PullFromSong_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Pull Song:", "Would you like to remove " + lblSongMissed.Text + " from the setlist of " + lblPerformerName.Text + "?", "Yes", "No");
        if (answer == true)
        {
           bool success = MauiProgram.BusinessLogic.RemoveSongFromSetlist(_performer, _song);
           if (success == true)
           {
                await DisplayAlert("Remove Song From Setlist", "Successfully removed " + lblSongMissed.Text + " from the setlist of " + lblPerformerName.Text, "OK");
           } else
           {
                await DisplayAlert("Remove Song From Setlist", "Failed to remove " + lblSongMissed.Text + " from the setlist of " + lblPerformerName.Text, "OK");
            } 
        }

      
    }
}