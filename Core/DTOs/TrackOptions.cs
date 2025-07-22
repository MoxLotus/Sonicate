using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public abstract class TrackOptions(int index)
{
    public int Index { get; } = index;
    public bool Copy { get; set; } = true;
}
