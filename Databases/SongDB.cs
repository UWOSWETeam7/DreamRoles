﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prototypes.Model.Interfaces;

namespace Prototypes.Databases;

internal class SongDB : ISongDB
{
    public SongDB(string title, string artist, int duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }

    public string Title { get; set; }
    public string Artist { get; set; }
    public int Duration { get; set; }
}