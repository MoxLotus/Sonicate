using System.ComponentModel;


namespace Sonicate.GUI.ViewModels.Utility;

public class Flagged<T>(T value) : INotifyPropertyChanged
{
    public T Value { get; init; } = value;

    private bool? _isFlagged = true;
    public bool? IsFlagged
    {
        get => _isFlagged;
        set
        {
            if (value == _isFlagged) return;
            _isFlagged = value;
            OnPropertyChanged(nameof(IsFlagged));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
