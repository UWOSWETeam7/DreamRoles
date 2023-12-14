using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoRehearsalsPage : ContentPage
{
    public ChoreoRehearsalsPage()
    {
        InitializeComponent();
        BindingContext = new RehearsalsViewModel();
    }

    private async void RehearsalPerformers_Clicked(object sender, EventArgs e)
    {
        Rehearsal rehearsal = CVRehearsals.SelectedItem as Rehearsal;

        if (rehearsal == null)
        {
            await DisplayAlert(null, "You must select a rehearsal to see the performers first", "Okay");
            return;
        }

        await Navigation.PushAsync(new ChoreoRehearsalPage(rehearsal));
    }

    private async void SignOutButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new WelcomePage());
        Navigation.RemovePage(this);
    }
}