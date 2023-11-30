using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Model
{
    class RehearsalMember: INotifyPropertyChanged
    {
        private int _id;
        private DateTime _rehearsalTime;
        private String _songTitle;
        private Boolean _checkedIn;
        private String _status;
        public event PropertyChangedEventHandler PropertyChanged;


        public RehearsalMember(int id, DateTime rehearsalTime, String songTitle, Boolean checkedIn, String status)
        {
             
        }   

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime RehearsalTime
        {
            get { return _rehearsalTime; }
            set { _rehearsalTime = value; OnPropertyChanged(nameof(RehearsalTime)); }
        }

        public String SongTitle
        {
            get { return _songTitle; }
            set { _songTitle = value; OnPropertyChanged(nameof(SongTitle)); }
        }

        public Boolean CheckedIn
        {
            get { return _checkedIn; }
            set { _checkedIn = value; OnPropertyChanged(nameof(CheckedIn)); }
        }

        public String Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(Status)); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
