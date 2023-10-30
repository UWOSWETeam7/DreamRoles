using System.Numerics;

namespace Prototypes.UI;

public partial class EditContactInfoPopup : ContentPage
{
	public EditContactInfoPopup()
	{
		InitializeComponent();
	}
    private void EditContactInfo(object sender, EventArgs e)
    {
        int userId = 1223;
        String phoneNumber = phoneNumberEntry.Text;
        string email = emailEntry.Text;
        MauiProgram.StageManagerBL.EditPerformerContact(userId, phoneNumber, email);
    }
}