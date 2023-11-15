using Prototypes.Model;
using Prototypes.Databases;
using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class SelectNamePage : ContentPage
{
    Performer performer;
    Database db = new Database();
    public SelectNamePage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();

        //show performer names in the dropdown
        //once user selects their name, navigate to PerformerHomePage.xaml for that specific performer
    }

    private void OnNameSelected(object sender, EventArgs e)
    {
        var label = (Label)sender;
        performer = (Performer)label.BindingContext;
    }
    private void OnNextClicked(object sender, EventArgs e)
    {
        db.CheckInPerformer(performer, "checked in");
        Navigation.PushAsync(new PerformerHomePage(performer));
        //change checked in status of this performer
    }


}