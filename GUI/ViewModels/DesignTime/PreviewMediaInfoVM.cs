using Avalonia.Platform.Storage;
using Sonicate.Core.DTOs;
using Sonicate.GUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sonicate.Core.DTOs.MediaInfo;

namespace Sonicate.GUI.ViewModels.DesignTime;

public class PreviewMediaInfoVM : MediaInfoVM
{
    public PreviewMediaInfoVM()
        : base(
            new FileDescriptor(
                "Sample Video File with a Very Long Name to Test UI Layouts.mp4",
                new Uri("file:///C:/Videos/SampleVideoFileWithAVeryLongNameToTestUILayouts.mp4")
            ),
            new DummyMediaInfo
            {
                Format = "Matroska",
                Name = "Preview",
                Duration = TimeSpan.FromMinutes(92),
            },
            new DummyScrollSyncService()
        )
    {
    }

    private class DummyMediaInfo : MediaInfo
    {
        public DummyMediaInfo()
        {
            AddTrack(PreviewVideoTrackInfoVM.PreviewInfo);
            AddTrack(PreviewAudioTrackInfoVM.PreviewInfo);
            AddTrack(PreviewAudioTrackInfoVM.PreviewInfo2);
            AddTrack(new TrackInfo
            {
                Codec = "SRT",
                Language = "eng",
                Type = TrackInfo.TrackType.Subtitle
            });
        }
    }
    private class DummyScrollSyncService : ScrollSyncService
    {
    }

}
