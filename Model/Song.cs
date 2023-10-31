using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Model
{
    public class Song
    {
        private int _setlistId;
        private String _title;
        private String _artist;
        private int _duration;
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
            set { _setlistId = value; }
        }

        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public String Artist
        {
            get { return _artist; }
            set { _artist = value; }
        }

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
    }
}
