using Prototypes.Model;
using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class SelectNamePage : ContentPage
{
    public SelectNamePage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();

        //show performer names in the dropdown
        //once user selects their name, navigate to PerformerHomePage.xaml
    }

    private void OnNameSelected(object sender, EventArgs e)
    {
        var label = (Label)sender;
        var performer = (Performer)label.BindingContext;
        Navigation.PushAsync(new PerformerHomePage(performer));
    }


}