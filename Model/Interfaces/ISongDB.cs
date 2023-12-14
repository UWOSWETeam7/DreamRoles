using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Model.Interfaces;

public interface ISongDB
{
    string Title { get; set; }
    string Notes {  get; set; }
}
