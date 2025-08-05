using Avalonia.Platform.Storage;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class MediaInfoViewModel(IStorageFile file, MediaInfo media) : ViewModelBase
{
    public IStorageFile File { get; } = file;
    public MediaInfo Media { get; } = media;
}
