using Prototypes.Model;
using System.Numerics;

namespace Prototypes.UI;

public partial class EditContactInfoPopup : ContentPage
{
    private Performer _performer;
	public EditContactInfoPopup(Performer performer)
	{
		_performer = performer;
        InitializeComponent();
	}
    private void EditContactInfo(object sender, EventArgs e)
    {
        
        
        int userId = _performer.Id;
        String phoneNumber = phoneNumberEntry.Text;
        string email = emailEntry.Text;
        MauiProgram.StageManagerBL.EditPerformerContact(userId, phoneNumber, email);
        Navigation.PopAsync();
    }
}