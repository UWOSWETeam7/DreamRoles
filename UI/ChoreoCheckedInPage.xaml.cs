using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoCheckedInPage : ContentPage
{
	public ChoreoCheckedInPage()
	{
        InitializeComponent();
        BindingContext = new SearchBarCheckedInPerformerViewModel();
    }
}