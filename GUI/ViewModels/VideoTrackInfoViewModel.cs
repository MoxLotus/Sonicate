using Sonicate.Core.DTOs;
using System;

namespace Sonicate.GUI.ViewModels;

public class VideoTrackInfoViewModel(VideoTrackInfo track) : TrackInfoViewModel(track)
{
    public VideoTrackInfo VideoTrack { get; } = track;
}
