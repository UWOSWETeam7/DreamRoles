using Prototypes.Model;

namespace Prototypes.UI;
//@author: Kaia Thern
public partial class PerformersOfSongPage : ContentPage
{
    public PerformersOfSongPage()
    {
        InitializeComponent();
        BindingContext = new SearchBarPerformerViewModel();
    }
}