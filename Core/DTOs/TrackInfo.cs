using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class TrackInfo
{
    public TrackType Type { get; set; }
    public string Codec { get; set; } = "";
    public string Language { get; set; } = "";
    public long Bitrate { get; set; }

    public enum TrackType
    {
        Video,
        Audio,
        Subtitle,
        Unknown,
    }
}
