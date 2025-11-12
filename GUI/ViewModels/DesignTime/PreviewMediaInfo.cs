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

public class PreviewMediaInfo : MediaInfoViewModel
{
    public PreviewMediaInfo()
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
            AddTrack(new TrackInfo { Codec = "H.264", Language = "und", Type = TrackInfo.TrackType.Video });
            AddTrack(new TrackInfo { Codec = "AAC", Language = "eng", Type = TrackInfo.TrackType.Audio});
            AddTrack(new TrackInfo { Codec = "Opus", Language = "jap", Type = TrackInfo.TrackType.Audio });
            AddTrack(new TrackInfo { Codec = "SRT", Language = "eng", Type = TrackInfo.TrackType.Subtitle });
        }
    }

    public class DummyFile
    {
        public string Name { get; } = "Hello World! File names are often quite long.";
    }

    private class DummyScrollSyncService : ScrollSyncService
    {
        // Stubbed behavior if needed
    }

}
