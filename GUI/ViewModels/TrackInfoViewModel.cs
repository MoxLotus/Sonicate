using Avalonia;
using ReactiveUI;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class TrackInfoViewModel(MediaInfo.TrackInfo track) : ViewModelBase
{
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }
    public MediaInfo.TrackInfo Track { get; } = track;
    public Thickness Margin { get; } = new(
        track.Type switch
        {
            MediaInfo.TrackInfo.TrackType.Video => 0,
            MediaInfo.TrackInfo.TrackType.Audio => 25,
            MediaInfo.TrackInfo.TrackType.Subtitle => 50,
            _ => 90
        }
        , 0, 5, 0
    );
}
