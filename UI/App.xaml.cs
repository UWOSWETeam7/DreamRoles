using Prototypes.Model;
using Prototypes.Model.Interfaces;
using System.Collections.ObjectModel;

namespace Prototypes.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ObservableCollection<ISongDB> song = new ObservableCollection<ISongDB>();
            Performer p = new Performer(122, "Keenan", "Marco", song, "Email", "Phone", 0);
            MainPage = new NavigationPage(new PerformerHomePage(p));
            ;
        }
    }
}