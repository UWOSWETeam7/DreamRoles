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
        if (email == null)
        {
            email = "";

        }
        if (phoneNumber == null)
        {
            phoneNumber = "";
        }

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
            String answer = MauiProgram.BusinessLogic.AddPerformer(firstName, lastName, null, email, phoneNumber);
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