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
        //Get the id from the performer
        int userId = _performer.Id;

        //Get the information from the entries
        String phoneNumber = phoneNumberEntry.Text;
        String email = emailEntry.Text;

        //If the user did not enter something for the phoneNumber or email entries grab what is currently set for the performer
        if(phoneNumber == null)
        {
            phoneNumber = _performer.PhoneNumber;
        }
        if(email == null)
        {
            email = _performer.Email;
        }

        //Ask the BusinessLogic to change the contact information of this performers
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