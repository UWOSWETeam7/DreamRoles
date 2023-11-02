using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Telecom;
using Prototypes.Model.Interfaces;
namespace Prototypes.Model
{
    public class Song : ISongDB, INotifyPropertyChanged
    {
        private int _setlistId;
        private String _title;
        private String _artist;
        private int _duration;
        public event PropertyChangedEventHandler PropertyChanged;
        public Song(int setlistId, String title, String artist, int duration)
        {
            SetlistId = setlistId;
            Title = title;
            Artist = artist;
            Duration = duration;
        }

        public int SetlistId
        {
            get { return _setlistId; }
            set { _setlistId = value; OnPropertyChanged(nameof(SetlistId)); }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        public String Artist
        {
            get { return _artist; }
            set { _artist = value; OnPropertyChanged(nameof(Artist)); }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public int DurationMinutes
        {
            get { return _duration / 60; }
        }
        public int DurationSeconds
        {
            get { return _duration % 60; }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
