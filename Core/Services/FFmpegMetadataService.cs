using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Enums;
using Sonicate.Core.Extensions;
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
    public async Task<MediaInfo> GetMetadataAsync(string filePath)
    {
        Debug.WriteLine("Getting media info for file: " + filePath);
        IMediaAnalysis mediaInfo = await FFProbe.AnalyseAsync(filePath);

        MediaInfo containerInfo = new()
        {
            Format = mediaInfo.Format.FormatLongName,
            Name = mediaInfo.Format.Tags?.GetValueOrEmpty("title") ?? string.Empty,
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
                    AudioStream => MediaInfo.TrackInfo.TrackType.Audio,
                    SubtitleStream => MediaInfo.TrackInfo.TrackType.Subtitle,
                    VideoStream => MediaInfo.TrackInfo.TrackType.Video,
                    _ => MediaInfo.TrackInfo.TrackType.Unknown,
                },
                Codec = stream.CodecName,
                Language = stream.Language ?? string.Empty,
            });
        }

        return containerInfo;
    }
}
