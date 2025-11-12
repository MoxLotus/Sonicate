using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class VideoTrackInfo : TrackInfo
{
    public required int Width { get; set; }
    public required int Height { get; set; }
    public required double FrameRate { get; set; }
}
