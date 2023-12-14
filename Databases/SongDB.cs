using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototypes.Model.Interfaces;

namespace Prototypes.Databases;

internal class SongDB : ISongDB
{
    public SongDB(string title, string notes)
    {
        Title = title;
        Notes = notes;
    }

    public string Title { get; set; }
    public string Notes { get; set; }
}
