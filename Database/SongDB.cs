using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototypes.Model.Interfaces;

namespace Prototypes.Database;

internal class SongDB : ISongDB
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Duration { get; set; }
}
