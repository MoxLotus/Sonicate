using ReactiveUI;
using Sonicate.GUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class AudioVMRow : INotifyPropertyChanged
{
    public List<AudioTrackInfoVM> AudioTracks { get; }
    public List<string> Languages { get; }
    public string Codec { get; }
    public string Language { get; }
    public int Channels { get; }
    public string Layout { get; }
    private bool? _selected = true;

    public bool? Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (value != null) AudioTracks.ForEach(t => t.Selected = (bool)value);
            OnPropertyChanged(nameof(Selected));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public AudioVMRow(List<AudioTrackInfoVM> tracks)
    {
        AudioTracks = tracks;
        AudioTracks.ForEach(t => t.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(AudioTrackInfoVM.Selected))
                UpdateSelected();
        });

        List<string> codecs = [.. tracks.Select(t => t.AudioTrack.Codec).Distinct()];
        Languages = [.. tracks.Select(t => t.AudioTrack.Language).Distinct()];
        List<int> channels = [.. tracks.Select(t => t.AudioTrack.Channels).Distinct()];
        List<string> layouts = [.. tracks.Select(t => t.AudioTrack.Layout).Distinct()];
        Codec = codecs.Count == 1 ? codecs[0] : "varies";
        Language = Languages.Count == 1 ? Languages[0] : "varies";
        Channels = channels.Count == 1 ? channels[0] : -1;
        Layout = layouts.Count == 1 ? layouts[0] : "varies";
    }

    private void UpdateSelected()
    {
        int count = AudioTracks.Where(t => t.Selected).Count();
        bool? update = null;
        if (count == 0)
            update = false;
        else if (count == AudioTracks.Count)
            update = true;

        if (update != _selected)
        {
            _selected = update;
            OnPropertyChanged(nameof(Selected));
        }
    }
}
