using ReactiveUI;

namespace Sonicate.GUI.Services;

public class ScrollSyncService : ReactiveObject
{
    private double _offset;
    public double Offset
    {
        get => _offset;
        set => this.RaiseAndSetIfChanged(ref _offset, value);
    }
}
