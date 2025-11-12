using Avalonia;
using ReactiveUI;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class TrackInfoViewModel(TrackInfo track) : SelectableViewModel
{
    public TrackInfo Track { get; } = track;
    public Thickness Margin { get; } = new(
        track.Type switch
        {
            TrackInfo.TrackType.Video => 0,
            TrackInfo.TrackType.Audio => 25,
            TrackInfo.TrackType.Subtitle => 50,
            _ => 90
        }
        , 0, 5, 0
    );
}
