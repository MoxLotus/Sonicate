using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class SubtitleVM : MainVM.Child
{
    public ObservableCollection<SubtitleVMItem> Tracks { get; private set; } = [];
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set
        {
            foreach (var track in Tracks)
                track.Selected = value;
            this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }
    public void AnalyzeSubtitleTracks()
    {
        Tracks.Clear();

        // We grab the ViewModels so that we can select and deselect them in the UI.
        List<List<TrackInfoVM>> matrix =
            [.. Parent.FileSelect.MediaFiles
                .Select(file =>
                    file.Tracks
                    .Where(t =>
                        t.Track.Type == Core.DTOs.TrackInfo.TrackType.Subtitle
                    ).ToList()
                )
            ];

        int rows = matrix.Max(list => list.Count);
        for (int r = 0; r < rows; r++)
            Tracks.Add(new([.. matrix.Where(c => r < c.Count).Select(c => c[r])]));
    }
}
