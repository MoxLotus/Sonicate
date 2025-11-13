using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels.DesignTime;

public class PreviewTrackInfoVM : TrackInfoVM
{
    public static TrackInfo PreViewSubtitleInfo { get; } = new TrackInfo
    {
        Codec = "SRT",
        Language = "eng",
        Type = TrackInfo.TrackType.Subtitle
    };
    public PreviewTrackInfoVM()
        : base(PreViewSubtitleInfo)
    {
    }
}
