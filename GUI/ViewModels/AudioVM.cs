using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class AudioVM : MainVM.Child
{
    public ObservableCollection<TrackInfoVM> Tracks { get; private set; } = [];
    public void AnalyzeAudioTracks(){
        Tracks.Clear();

        // We grab the ViewModels so that we can select and deselect them in the UI.
        List<List<AudioTrackInfoVM>> matrix =
            [.. Parent.FileSelect.MediaFiles
                .Select(file =>
                    file.Tracks.OfType<AudioTrackInfoVM>()
                    .ToList()
                )
            ];

        int rows = matrix.Max(list => list.Count);
        Debug.WriteLine($"AudioVM: Analyzing {matrix.Count} files with up to {rows} audio tracks.");

        for (int r = 0; r < rows; r++)
        {
            bool sameCodec = true;
            bool sameChannels = true;
            bool sameLanguage = true;
            string codec = matrix[0][r].Track.Codec ?? "";
            int channels = matrix[0][r].AudioTrack.Channels;
            string language = matrix[0][r].Track.Language ?? "";
            for (int c = 1; c < matrix.Count; c++)
            {
                if (matrix[c].Count <= r)
                    break;
                AudioTrackInfoVM candidate = matrix[c][r];
                if (sameCodec)
                    if (matrix[c].Count > r && !codec.Equals(candidate.Track.Codec))
                    {
                        sameCodec = false;
                        codec = "varies";
                    }

                if (sameChannels)
                    if (matrix[c].Count > r && channels != candidate.AudioTrack.Channels)
                    {
                        sameChannels = false;
                        channels = -1;
                    }

                if (sameLanguage)
                    if (matrix[c].Count > r && !language.Equals(candidate.Track.Language))
                    {
                        sameLanguage = false;
                        language = "varies";
                    }

                if (!sameCodec && !sameChannels)
                    break;
            }

            Tracks.Add(new AudioTrackInfoVM(
                new AudioTrackInfo()
                {
                    Codec = codec,
                    Channels = channels,
                    Language = language,
                }
            ));
        }
    }
}
