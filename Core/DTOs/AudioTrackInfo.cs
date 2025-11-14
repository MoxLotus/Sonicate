using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class AudioTrackInfo : TrackInfo
{
    public required int Channels { get; init; }
    public string Layout { get; init; }
}
