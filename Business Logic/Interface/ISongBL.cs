﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Business_Logic
{
    interface ISongBL
    {
        String Title { get; set; }
        String Artist { get; set; }
        int Duration { get; set; }
    }
}
