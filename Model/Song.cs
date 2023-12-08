using System.ComponentModel;
using System.Windows.Markup;
using Prototypes.Model.Interfaces;
namespace Prototypes.Model
{
    public class Song : ISongDB, INotifyPropertyChanged
    {
        private String _title;
        private String _notes;
        public event PropertyChangedEventHandler PropertyChanged;
        public Song(String title, String notes)
        {
            Title = title;
            Notes = notes;
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public String Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(nameof(Title)); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
