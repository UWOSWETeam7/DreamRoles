namespace Prototypes.UI;

public partial class AddPerformerPopup : ContentPage
{
	public AddPerformerPopup()
	{
		InitializeComponent();
	}
    private void AddPerformer(object sender, EventArgs e)
    {
        //Grabs the entries from the page
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        string phoneNumber = phoneNumberEntry.Text;
        string email = emailEntry.Text;

        //If null set to a string so the business logic can test it's length without crashing
        if (email == null)
        {
            email = "";

        }
        if (phoneNumber == null)
        {
            phoneNumber = "";
        }

        //Check to make sure these entries are filled in, if not send a alert
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
            //Ask BusinessLogic to add a performer
            var answer = MauiProgram.BusinessLogic.AddPerformer(firstName, lastName, null, email, phoneNumber);
            
            //It work and wil go to the preview page
            if (answer.ResultMessage == null)
            {
                Navigation.PopAsync();
            }
            //Failed so display a alert
            else
            {
                DisplayAlert("Error", answer.ResultMessage, "Ok");
            }
        }
    }
}