using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class ChoreoHomePage : ContentPage
{
    public ChoreoHomePage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();
    }
}