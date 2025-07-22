using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class InputOptions(string filename)
{
    public string Filename { get; set; } = filename;
    internal List<TrackOptions> Tracks { get; set; } = [];

    public void AddTrack(TrackOptions track)
    {
        Tracks.Add(track);
    }
}
