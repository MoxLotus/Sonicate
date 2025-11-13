using Sonicate.Core.DTOs;
using System;
using System.Runtime.ConstrainedExecution;

namespace Sonicate.GUI.ViewModels;

public class VideoTrackInfoVM(VideoTrackInfo track) : TrackInfoVM(track)
{
    public VideoTrackInfo VideoTrack { get; } = track;
}
