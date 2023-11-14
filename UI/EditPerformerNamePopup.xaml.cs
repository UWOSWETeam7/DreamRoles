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
        MauiProgram.BusinessLogic.EditPerformerName(userId, firstName, lastName);
        Navigation.PopAsync();

    }
}