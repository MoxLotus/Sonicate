using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class MediaInfoViewModel(MediaInfo media) : ViewModelBase
{
    public MediaInfo Media { get; } = media;
}
