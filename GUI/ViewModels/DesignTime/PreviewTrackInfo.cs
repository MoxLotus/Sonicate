using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels.DesignTime;

public class PreviewTrackInfo : TrackInfoViewModel
{
    public PreviewTrackInfo()
        : base(new TrackInfo
        {
            Codec = "Opus",
            Language = "eng",
            Type = TrackInfo.TrackType.Audio
        })
    {
    }
}
