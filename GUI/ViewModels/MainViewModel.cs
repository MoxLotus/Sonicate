using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using GUI.Services;

namespace GUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public FileSelectViewModel FileSelect { get; } = new();
    public TranscodeViewModel Transcode { get; } = new();
}
