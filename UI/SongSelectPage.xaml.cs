using Prototypes.Model;
using System.Diagnostics.Contracts;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class SongSelectPage : ContentPage
{
    private Performer _performer;
    public String userType; 
    public SongSelectPage(String userType, Performer performer)
    {
        InitializeComponent();
        BindingContext = MauiProgram.BusinessLogic;
        _performer = performer;
        this.userType = userType;
    }
    public async void OnDone_Clicked (object sender, EventArgs e)
    {
        if (userType == "performer")
        {
            await Navigation.PushAsync(new PerformerHomePage());
        }
        else if (userType == "choreographer")
        {
            await Navigation.PushAsync(new ChoreoHomePage());   
        }
    }

}