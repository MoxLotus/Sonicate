using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels.DesignTime;

public class PreviewVideoTrackInfoVM : VideoTrackInfoVM
{
    public static VideoTrackInfo PreviewInfo { get; } = new VideoTrackInfo
    {
        Codec = "H.264",
        Language = "und",
        Type = TrackInfo.TrackType.Video,
        Width = 1920,
        Height = 1080,
        FrameRate = 23.997
    };
    public PreviewVideoTrackInfoVM()
        : base(PreviewInfo)
    {
    }
}
