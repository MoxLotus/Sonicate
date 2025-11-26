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
    private bool? _selected = true;
    public bool? Selected
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

        // When any track's Selected property changes, we need to update the overall Selected property.
        foreach (AudioVMRow row in Tracks)
        {
            row.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != nameof(AudioVMRow.Selected))
                    return;

                bool? next = null;
                if (Tracks.All(t => t.Selected == true))
                    next = true;
                else if (Tracks.All(t => t.Selected == false))
                    next = false;
                this.RaiseAndSetIfChanged(ref _selected, next);
            };
        }

        AvailableLanguages.Clear();
        Tracks.SelectMany(t => t.Languages).Distinct().ToList().ForEach(i => AvailableLanguages.Add(new(i)));

        foreach (var lang in AvailableLanguages)
        {
            // When the flag for a language changes, we need to update all tracks with that language.
            lang.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName != nameof(Flagged<string>.IsFlagged))
                    return;

                if (s is not Flagged<string> source)
                    return;

                // Bugfix: Snapshot the flag to avoid reentrancy changing IsFlagged while we run.
                var flag = source.IsFlagged;
                if (flag is null)
                    return;

                foreach (var track in Tracks)
                    if (track.Language.Equals(source.Value))
                        track.Selected = flag;
                    else if (track.Language.Equals("varies"))
                        foreach (var t in track.AudioTracks)
                            if (t.Track.Language.Equals(source.Value))
                                t.Selected = flag.Value;
            };

            // When any track with this language changes, we need to update the flag for that language.
            Tracks
                .SelectMany(t => t.AudioTracks)
                .Where(t => t.Track.Language.Equals(lang.Value))
                .ToList()
                .ForEach(t => t.PropertyChanged += (s, e) =>
                {
                    if (s is not Flagged<string> source)
                        return;
                    if (e.PropertyName != nameof(Flagged<string>.IsFlagged))
                        return;
                    var allTracks = Tracks.SelectMany(tt => tt.AudioTracks)
                                          .Where(tt => tt.Track.Language.Equals(lang.Value))
                                          .ToList();
                    if (allTracks.All(tt => tt.Selected == true))
                        lang.IsFlagged = true;
                    else if (allTracks.All(tt => tt.Selected == false))
                        lang.IsFlagged = false;
                    else
                        lang.IsFlagged = null;
                });
        }
    }
}
