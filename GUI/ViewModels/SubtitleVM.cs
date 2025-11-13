using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class SubtitleVM : MainVM.Child
{
    public ObservableCollection<SubtitleVMItem> TrackVMsVM { get; private set; } = [];
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set
        {
            foreach (var trackVM in TrackVMsVM)
                trackVM.Selected = value;
            this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }

    public ObservableCollection<string> Languages { get; private set; } = [];
    private string? _selectedLanguage;
    public string? SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (value != null) IsolateLanguage(value);
            this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
        }
    }
    private string? _selectedLanguage2;
    public string? SelectedLanguage2
    {
        get => _selectedLanguage2;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguage2, value);
    }

    public void AnalyzeSubtitleTracks()
    {
        TrackVMsVM.Clear();

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
        {
            List<TrackInfoVM> trackVMs = [.. matrix.Where(c => r < c.Count).Select(c => c[r])];
            foreach (var trackVM in trackVMs)
                if (!Languages.Contains(trackVM.Track.Language))
                    Languages.Add(trackVM.Track.Language);
            TrackVMsVM.Add(new(trackVMs));
        }
    }

    public void IsolateLanguage(string language)
    {
        Debug.WriteLine($"Isolating language: {language}");
        foreach (var track in TrackVMsVM)
            if (track.Language.Equals(language))
                track.Selected = true;
            else if (!track.Language.Equals("varies"))
                track.Selected = false;
            else
                foreach (var t in track.SubtitleTrackVMs)
                    if (t.Track.Language.Equals(language))
                        t.Selected = true;
                    else
                        t.Selected = false;
    }

    public void AddLanguage()
    {
        Debug.WriteLine($"Adding language: {SelectedLanguage2}");
        foreach (var track in TrackVMsVM)
            if (track.Language.Equals(SelectedLanguage2))
                track.Selected = true;
            else if (track.Language.Equals("varies"))
                foreach (var t in track.SubtitleTrackVMs)
                    if (t.Track.Language.Equals(SelectedLanguage2))
                        t.Selected = true;
    }

    public void RemoveLanguage()
    {
            Debug.WriteLine($"Removing language: {SelectedLanguage2}");
            foreach (var track in TrackVMsVM)
            if (track.Language.Equals(SelectedLanguage2))
                track.Selected = false;
            else if (track.Language.Equals("varies"))
                foreach (var t in track.SubtitleTrackVMs)
                    if (t.Track.Language.Equals(SelectedLanguage2))
                        t.Selected = false;
    }
}
