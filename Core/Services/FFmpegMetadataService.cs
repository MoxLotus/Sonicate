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
            TrackInfo trackInfo = stream switch
            {
                VideoStream videoStream =>
                   new VideoTrackInfo()
                   {
                       Type = TrackInfo.TrackType.Video,
                       Width = videoStream.Width,
                       Height = videoStream.Height,
                       FrameRate = videoStream.FrameRate,
                   },
                AudioStream audioStream =>
                    new AudioTrackInfo()
                    {
                        Type = TrackInfo.TrackType.Audio,
                        Channels = audioStream.Channels,
                        Layout = audioStream.ChannelLayout,
                    },
                SubtitleStream subtitleStream =>
                    new()
                    {
                        Type = TrackInfo.TrackType.Subtitle,
                        Codec = stream.CodecName,
                        Language = stream.Language ?? string.Empty,
                        Bitrate = stream.BitRate,
                    },
                _ =>
                    new()
                    {
                        Type = TrackInfo.TrackType.Unknown,
                    }
            };
            trackInfo.Codec = stream.CodecName;
            trackInfo.Language = stream.Language ?? string.Empty;
            trackInfo.Bitrate = stream.BitRate;

            containerInfo.AddTrack(trackInfo);
        }

        return containerInfo;
    }
}
