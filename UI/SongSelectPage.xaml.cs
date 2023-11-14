using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class SongSelectPage : ContentPage
{
    private Performer _performer;
    public SongSelectPage(Performer performer)
    {
        InitializeComponent();
        BindingContext = MauiProgram.BusinessLogic;
        _performer = performer;
    }
}