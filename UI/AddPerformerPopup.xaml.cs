namespace Prototypes.UI;

public partial class AddPerformerPopup : ContentPage
{
	public AddPerformerPopup()
	{
		InitializeComponent();
	}
    private void AddPerformer(object sender, EventArgs e)
    {
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        string phoneNumber = phoneNumberEntry.Text;
        string email = emailEntry.Text;
        if(firstName == null)
        {
            firstName = "";
        }
        if (lastName == null)
        {
            lastName = "";
        }
        if (email == null)
        {
            email = "";

        }
        if (phoneNumber == null)
        {
            phoneNumber = "0";
        }

        if (firstName == "")
        {
            DisplayAlert("Error", "Please enter a fist name.", "Ok");
        }
        else if (lastName == "")
        {
            DisplayAlert("Error", "Please enter a last name.", "Ok");
        }
        else
        {
            int newPhoneNumber  = Int32.Parse(phoneNumber);
            String answer = MauiProgram.BusinessLogic.AddPerformer(firstName, lastName, null, email, newPhoneNumber);
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