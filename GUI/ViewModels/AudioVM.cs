using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class AudioVM : MainVM.Child
{
    public void AnalyzeAudioTracks(){
        // We grab the ViewModels so that we can select and deselect them in the UI.
        List<List<TrackInfoVM>> matrix =
            [.. (from file in Parent.FileSelect.MediaFiles
             select
                 (from track in file.Tracks
                  where track.Track.Type == TrackInfo.TrackType.Audio
                  select track
                 ).ToList()
            )];

        int rows = 0;
        foreach (var list in matrix)
            if (list.Count > rows)
                rows = list.Count;

        ;
    }
}
