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

public class MainVM : ReactiveObject
{
    public FileSelectVM FileSelect { get; } = new();
    public TranscodeVM Transcode { get; } = new();
    public AudioVM Audio { get; } = new();
    public AudioVM Commentary { get; } = new();

    public MainVM()
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
        private MainVM? _parent;
        protected MainVM Parent => _parent ?? throw new InvalidOperationException("AudioViewModel not initialized.");
        public void Initialize(MainVM parent)
        {
            _parent ??= parent;
        }
    }
}
