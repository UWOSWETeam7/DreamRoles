using Microsoft.Maui.ApplicationModel.Communication;
using Prototypes.Model;

namespace Prototypes.UI;

public partial class EditPerformerNamePopup : ContentPage
{
    private Performer _performer;
	public EditPerformerNamePopup(Performer performer)
	{
        _performer = performer;
		InitializeComponent();
	}

    private void EditPerformerName(object sender, EventArgs e)
    {


        int userId = _performer.Id;
        String firstName = firstNameEntry.Text;
        String lastName = lastNameEntry.Text;
        if (firstName == null)
        {
            DisplayAlert("Error", "Please enter a fist name.", "Ok");
        }
        else if (lastName == null)
        {
            DisplayAlert("Error", "Please enter a last name.", "Ok");
        }
        else
        {
            String answer = MauiProgram.BusinessLogic.EditPerformerName(userId, firstName, lastName);
            if (answer == null)
            {
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", answer, "Ok");
            }
        }
    }
}