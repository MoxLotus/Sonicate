using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class SubtitleVMItem : ReactiveObject
{
    public List<TrackInfoVM> SubtitleTrackVMs { get; }
    public string Codec { get; init; }
    public string Language { get; init; }
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set
        {
            SubtitleTrackVMs.ForEach(t => t.Selected = value);
            this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }

    public SubtitleVMItem(List<TrackInfoVM> trackVMs)
    {
        SubtitleTrackVMs = trackVMs;
        List<string> codecs = [.. trackVMs.Select(t => t.Track.Codec).Distinct()];
        List<string> language = [.. trackVMs.Select(t => t.Track.Language).Distinct()];
        Codec = codecs.Count == 1 ? codecs[0] : "varies";
        Language = language.Count == 1 ? language[0] : "varies";
    }
}
