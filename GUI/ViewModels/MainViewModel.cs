using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Sonicate.GUI.Services;

namespace Sonicate.GUI.ViewModels;

public class MainViewModel : ReactiveObject
{
    public FileSelectViewModel FileSelect { get; } = new();
    public TranscodeViewModel Transcode { get; } = new();
    public AudioViewModel Audio { get; } = new();
    public AudioViewModel Commentary { get; } = new();

    public MainViewModel()
    {
        FileSelect.Initialize(this);
        Transcode.Initialize(this);
        Audio.Initialize(this);
        Commentary.Initialize(this);
    }

    /// <summary>
    /// Represents a base class for child view models that require initialization with a this view model.
    /// </summary>
    /// <remarks>
    /// This allows ViewModels instantiated here to have access to the rest of the application state.
    /// </remarks>
    public abstract class Child : ReactiveObject
    {
        private MainViewModel? _parent;
        protected MainViewModel Parent => _parent ?? throw new InvalidOperationException("AudioViewModel not initialized.");
        public void Initialize(MainViewModel parent)
        {
            _parent ??= parent;
        }
    }
}
