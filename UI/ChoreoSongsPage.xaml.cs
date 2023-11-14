using Prototypes.Model;

namespace Prototypes.UI;

public partial class ChoreoSongsPage : ContentPage
{
	public ChoreoSongsPage()
	{
		InitializeComponent();
        BindingContext = new SearchBarSongViewModel();
    }
}