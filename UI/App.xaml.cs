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
           // Performer performer = new Performer(1, "Wilson", "Ava", song,"avaWilson98@gmail.com", "9202347612", 0);
            MainPage = new NavigationPage(new WelcomePage());
            ;
        }
    }
}