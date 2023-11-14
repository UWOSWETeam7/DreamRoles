using Prototypes.Model;

namespace Prototypes.UI;
//@author: Keenan Marco
public partial class PerformerHomePage : ContentPage
{
    private Performer _performer;
    public PerformerHomePage(Performer performer)
    {
        InitializeComponent();
        _performer = performer;
    }
}