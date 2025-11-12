using Sonicate.Core.DTOs;
using System;

namespace Sonicate.GUI.ViewModels;

public class AudioTrackInfoViewModel(AudioTrackInfo track) : TrackInfoViewModel(track)
{
    public AudioTrackInfo AudioTrack { get; } = track;
}
