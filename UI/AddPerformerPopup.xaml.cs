namespace Prototypes.UI;

public partial class AddPerformerPopup : ContentPage
{
	public AddPerformerPopup()
	{
		InitializeComponent();
	}
    private void AddPerformer(object sender, EventArgs e)
    {
        int userId = Int32.Parse(userIdEntry.Text);
        string firstName = firstNameEntry.Text;
        string lastName = lastNameEntry.Text;
        string phoneNumber = phoneNumberEntry.Text;
        string email = emailEntry.Text;

        MauiProgram.StageManagerBL.AddPerformer(userId, firstName, lastName, null, email, phoneNumber);
        Navigation.PopAsync();
    }
}