using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Business_Logic;

internal class SongBL : ISongBL
{
    public string Title { get; set;}
    public string Artist { get; set; }
    public int Duration { get; set; }
}
