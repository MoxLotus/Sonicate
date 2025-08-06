using Avalonia.Platform.Storage;
using ReactiveUI;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class MediaInfoViewModel(IStorageFile file, MediaInfo media) : ViewModelBase
{
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }
    public IStorageFile File { get; } = file;
    public MediaInfo Media { get; } = media;
}
