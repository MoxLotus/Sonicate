using Avalonia.Platform.Storage;
using ReactiveUI;
using Sonicate.Core.DTOs;
using Sonicate.GUI.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sonicate.GUI.ViewModels;

public class MediaInfoViewModel(FileDescriptor file, MediaInfo media, ScrollSyncService scrollSync) : ViewModelBase
{
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }
    public FileDescriptor File { get; } = file;
    public MediaInfo Media { get; } = media;
    public ScrollSyncService ScrollSync { get; } = scrollSync;

    public ObservableCollection<TrackInfoViewModel> Tracks { get; } = new(
        media.Tracks.Select(t => new TrackInfoViewModel(t))
    );
}
