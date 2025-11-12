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
    private readonly List<TrackInfo> _tracks = [];
    public IReadOnlyList<TrackInfo> Tracks
    {
           get => _tracks.AsReadOnly();
    }
    public void AddTrack(TrackInfo track)
    {
        _tracks.Add(track);
    }
}
