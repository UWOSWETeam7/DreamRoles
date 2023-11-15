using Prototypes.Model;
using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class SelectNamePage : ContentPage
{
    public ObservableCollection<Performer> Performers { get; set; }
    String userType;
    Performer performer; 
    public SelectNamePage(string userType)
    {
        InitializeComponent();
        this.userType = userType; 
    }
    public async void OnNext_Clicked(object sender, EventArgs e)
    {
        if (userType == "performer")
        {
            await Navigation.PushAsync(new SongSelectPage(userType, performer));
        }
        else if (userType == "stagemanager")
        {
            await Navigation.PushAsync(new ManagerHomePage());
        }
        else if (userType == "choreographer")
        {
            await Navigation.PushAsync(new SongSelectPage(userType, performer)); 
        }
    }
}