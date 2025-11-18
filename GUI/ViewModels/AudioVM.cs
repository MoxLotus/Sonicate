using Avalonia.Controls;
using ReactiveUI;
using Sonicate.Core.DTOs;
using Sonicate.GUI.ViewModels.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class AudioVM : MainVM.Child
{
    public ObservableCollection<AudioVMRow> Tracks { get; private set; } = [];
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set
        {
            foreach (var trackVM in Tracks)
                trackVM.Selected = value;
            this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }
    public ObservableCollection<Flagged<string>> AvailableLanguages { get; private set; } = [];

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
        for (int r = 0; r < rows; r++)
            Tracks.Add(new([.. matrix.Where(c => r < c.Count).Select(c => c[r])]));

        AvailableLanguages.Clear();
        Tracks.SelectMany(t => t.Languages).Distinct().ToList().ForEach(i => AvailableLanguages.Add(new(i)));

        foreach (var lang in AvailableLanguages)
            lang.PropertyChanged += (s, e) =>
            {
                if (s is Flagged<string> source)
                    foreach (var track in Tracks)
                        if (track.Language.Equals(source.Value))
                            track.Selected = source.IsFlagged;
                        else if (track.Language.Equals("varies"))
                            foreach (var t in track.AudioTracks)
                                if (t.Track.Language.Equals(source.Value))
                                    t.Selected = source.IsFlagged;
            };
    }
}
