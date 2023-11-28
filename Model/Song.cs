using System.ComponentModel;
using Prototypes.Model.Interfaces;
namespace Prototypes.Model
{
    public class Song : ISongDB, INotifyPropertyChanged
    {
        private String _title;
        public event PropertyChangedEventHandler PropertyChanged;
        public Song(String title)
        {
            Title = title;
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
