using Sonicate.Core.DTOs;
using System;

namespace Sonicate.GUI.ViewModels;

public class AudioTrackInfoVM(AudioTrackInfo track) : TrackInfoVM(track)
{
    public AudioTrackInfo AudioTrack { get; } = track;
}
