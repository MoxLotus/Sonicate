using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.Coordinators;

public interface IMediaProcessingCoordinator
{
    void Start(TaskOptions taskOptions);
}

public class MediaProcessingCoordinator : IMediaProcessingCoordinator
{
    public void Start(TaskOptions taskOptions)
    {
        foreach (var input in taskOptions.Inputs)
        {
            foreach (var track in input.Tracks)
            {
                switch (track)
                {
                    case AudioOptions audio:
                        //ProcessAudioTrack(input, audio);
                        break;
                    case SubtitleOptions subtitle:
                        //ProcessSubtitleTrack(input, subtitle);
                        break;
                    case VideoOptions video:
                        //ProcessVideoTrack(input, video);
                        break;
                    default:
                        throw new NotSupportedException($"Track type {track.GetType().Name} is not supported.");
                }
            }
        }
        throw new NotImplementedException();
    }
}
