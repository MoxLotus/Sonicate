using ReactiveUI;
using Sonicate.GUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class AudioVMItem: ReactiveObject
{
    public List<AudioTrackInfoVM> AudioTracks { get; }
    public string Codec { get; init; }
    public string Language { get; init; }
    public int Channels { get; init; }
    private bool _selected = true;
    public bool Selected
    {
        get => _selected;
        set
        {
            AudioTracks.ForEach(t => t.Selected = value);
            this.RaiseAndSetIfChanged(ref _selected, value);
        }
    }

    public AudioVMItem(List<AudioTrackInfoVM> tracks)
    {
        AudioTracks = tracks;
        List<string> codecs = [.. tracks.Select(t => t.AudioTrack.Codec).Distinct()];
        List<string> language = [.. tracks.Select(t => t.AudioTrack.Language).Distinct()];
        List<int> channels = [.. tracks.Select(t => t.AudioTrack.Channels).Distinct()];
        Codec = codecs.Count == 1 ? codecs[0] : "varies";
        Language = language.Count == 1 ? language[0] : "varies";
        Channels = channels.Count == 1 ? channels[0] : -1;
    }
}
