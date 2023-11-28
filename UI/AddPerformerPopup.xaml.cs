namespace Prototypes.UI;

public partial class AddPerformerPopup : ContentPage
{
	public AddPerformerPopup()
	{
		InitializeComponent();
	}
    private void AddPerformer(object sender, EventArgs e)
    {
        String firstName = firstNameEntry.Text;
        String lastName = lastNameEntry.Text;
        String phoneNumber = phoneNumberEntry.Text;
        String email = emailEntry.Text;
        if(firstName == null) 
        {
            DisplayAlert("Error", "Please enter a fist name.", "Ok");
        }
        else if(lastName == null) 
        {
            DisplayAlert("Error", "Please enter a last name.", "Ok");
        }

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