using Prototypes.Databases;
using Prototypes.Model;
namespace Prototypes.UI;

public partial class AddPerformerToRehearsalPage : ContentPage
{
    Performer _performer;
    Rehearsal _rehearsal;
	public AddPerformerToRehearsalPage(Rehearsal rehearsal)
	{
		InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();
        _rehearsal = rehearsal;
	}

    private void CVPerformers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _performer = CVPerformers.SelectedItem as Performer;
    }

    private void OnAddPerformerToRehearsal(object sender, EventArgs e)
    {
        if (_performer != null)
        {
            MauiProgram.BusinessLogic.AddPerformerToRehearsal(_performer, _rehearsal);
            Navigation.PopAsync();
        }
    }
}