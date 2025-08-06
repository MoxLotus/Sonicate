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
}
