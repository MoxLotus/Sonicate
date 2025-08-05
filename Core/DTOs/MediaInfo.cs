using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class MediaInfo
{
    public required string Format { get; set; }
    public required string Name { get; set; }
    public required TimeSpan Duration { get; set; }
    public int Chapters { get; internal set; }
    private List<TrackInfo> _tracks = [];
    public void AddTrack(TrackInfo track)
    {
        _tracks.Add(track);
    }   
    public class TrackInfo
    {
        public TrackType Type { get; set; }
        public required string Codec { get; set; }
        public required string Language { get; set; }

        public enum TrackType
        {
            Video,
            Audio,
            Subtitle,
            Unknown,
        }
    }
}
