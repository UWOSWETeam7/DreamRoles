using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class ManagerAlertPage : ContentPage
{
    /// <summary>
    /// ManagerAlertPage 
    /// </summary>
    /// <param name="currentPerformer"></param>
    public ManagerAlertPage(Performer currentPerformer)
    {
        InitializeComponent();
        lblPerformerName.Text = currentPerformer.FirstName + " " + currentPerformer.LastName;
        //Might be able to pull performer's title later
        lblTitle.Text = "Performer";
        lblAbsences.Text = currentPerformer.Absences.ToString();

    }
}