using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Model
{
    public class Rehearsal
    {
        private DateTime _time;
        private Song _song;

        public Rehearsal(DateTime time, Song song)
        {
            Time = time;
            Song = song;
        }

        public DateTime Time { get { return _time; } set { _time = value; } }
        public Song Song { get { return _song; } set { _song = value; } }


    }
}
