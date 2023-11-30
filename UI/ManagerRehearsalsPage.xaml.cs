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
}