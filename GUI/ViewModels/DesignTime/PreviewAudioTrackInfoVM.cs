using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels.DesignTime;

public class PreviewAudioTrackInfoVM : AudioTrackInfoVM
{
    public static AudioTrackInfo PreviewInfo { get; } = new AudioTrackInfo
    {
        Codec = "AAC",
        Language = "eng",
        Type = TrackInfo.TrackType.Audio,
        Channels = 6,
        Layout = "5.1",
    };
    public static AudioTrackInfo PreviewInfo2 { get; } = new AudioTrackInfo
    {
        Codec = "Opus",
        Language = "jap",
        Type = TrackInfo.TrackType.Audio,
        Channels = 2,
        Layout = "stereo",
    };
    public PreviewAudioTrackInfoVM()
        : base(PreviewInfo)
    {
    }
}
