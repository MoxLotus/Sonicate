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
    public ObservableCollection<AudioVMItem> Tracks { get; private set; } = [];
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

        var uniqueValuesPerRow = Enumerable.Range(0, matrix[0].Count)
            .Select(r => matrix.Select(col => col[r])
                .Select(track => track.Track.Codec)
                .Distinct()
                .ToList())
            .ToList();


        int rows = matrix.Max(list => list.Count);
        for (int r = 0; r < rows; r++)
            Tracks.Add(new([.. matrix.Where(c => r < c.Count).Select(c => c[r])]));
    }
}
