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

public class MainViewModel : ViewModelBase
{
    public FileSelectViewModel FileSelect { get; } = new();
    public TranscodeViewModel Transcode { get; } = new();
    public AudioViewModel Audio { get; } = new();
    public AudioViewModel Commentary { get; } = new();
}
