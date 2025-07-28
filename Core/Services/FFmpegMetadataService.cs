using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.Services;

public class FFmpegMetadataService : IVideoMetadataService
{
    public async Task<ContainerInfo> GetMetadataAsync(string filePath)
    {
        IMediaAnalysis mediaInfo = await FFProbe.AnalyseAsync(filePath);

        ContainerInfo containerInfo = new()
        {
            Format = mediaInfo.Format.FormatLongName,
            Duration = mediaInfo.Duration,
            Chapters = mediaInfo.Chapters.Count,
        };

        List<MediaStream> streams = [.. mediaInfo.VideoStreams, .. mediaInfo.AudioStreams, .. mediaInfo.SubtitleStreams];
        foreach (MediaStream stream in streams)
        {
            containerInfo.AddTrack(new()
            {
                Type = stream switch
                {
                    AudioStream => ContainerInfo.TrackInfo.TrackType.Audio,
                    SubtitleStream => ContainerInfo.TrackInfo.TrackType.Subtitle,
                    VideoStream => ContainerInfo.TrackInfo.TrackType.Video,
                    _ => ContainerInfo.TrackInfo.TrackType.Unknown,
                },
                Codec = stream.CodecName,
                Language = stream.Language ?? string.Empty,
            });
        }

        return containerInfo;
    }
}
