using Avalonia.Controls;
using ReactiveUI;
using Sonicate.Core.DTOs;
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
    public ObservableCollection<LanguageFilterItem> AvailableLanguages { get; private set; } = [];

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
        {
            lang.PropertyChanged += (s, e) =>
            {
                if (s is LanguageFilterItem source)
                    onCheck(source);
            };
        }
    }

    public class LanguageFilterItem(string language) : INotifyPropertyChanged
    {
        public string Language { get; init; } = language;

        private bool _selected = true;
        public bool Selected
        {
            get => _selected;
            set
            {
                if (value == _selected) return;
                _selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void onCheck(LanguageFilterItem source)
    {
        Debug.WriteLine($"Language filter changed: {source.Language} -> {source.Selected}");
    }
}
