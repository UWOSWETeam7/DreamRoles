using System.Collections.ObjectModel;

namespace Prototypes.UI;
//@author: Keerthana Ambati
public partial class SelectNamePage : ContentPage
{
    public ObservableCollection<Performer> Performers { get; set; }
    public SelectNamePage()
    {
        InitializeComponent();
        Performers = new ObservableCollection<Performer>();
    }
}