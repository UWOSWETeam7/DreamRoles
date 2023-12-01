using Prototypes.Model;

namespace Prototypes.UI;

public partial class ManagerRehearsalsPage : ContentPage
{
	public ManagerRehearsalsPage()
	{
		InitializeComponent();
		BindingContext = new RehearsalsViewModel();
		CVRehearsals.ItemsSource = MauiProgram.BusinessLogic.GetAllRehearsals();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new AddRehearsalPage());
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        Rehearsal rehearsal = CVRehearsals.SelectedItem as Rehearsal;

        if (rehearsal == null)
        {
            DisplayAlert(null, "You must select a rehearsal to delete first", "Okay");
            return;
        }

        var answer = await DisplayAlert("Confirm rehearsal delete", $"Delete Rehearsal for {rehearsal.Song.Title} at {rehearsal.Time}?", "Delete", "Cancel");

        if (answer)
        {
            var result = MauiProgram.BusinessLogic.DeleteRehersal(rehearsal);

            String alertTitle = "Failed to Delete Rehearsal";
            if (result.success)
            {
                alertTitle = "Successfully Deleted Rehearsal";
            }

            DisplayAlert(alertTitle, result.message, "Okay");
        }
    }
}