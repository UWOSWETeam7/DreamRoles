using Prototypes.Model;


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
        String email = emailEntry.Text;
        if(phoneNumber == null)
        {
            phoneNumber = _performer.PhoneNumber;
        }
        if(email == null)
        {
            email = _performer.Email;
        }
        String answer = MauiProgram.BusinessLogic.EditPerformerContact(userId, phoneNumber, email);
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