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
            MainPage = new NavigationPage(new PerformersOfSongPage());
            ;
        }
    }
}