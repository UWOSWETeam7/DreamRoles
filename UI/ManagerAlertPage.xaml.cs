using Prototypes.Model;

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

    }

    private void ShowManagerPerformerInfoPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ManagerPerformerInfoPage(_performer));
    }
}