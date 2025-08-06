using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using Sonicate.GUI.ViewModels;
using System;
using System.Reactive.Linq;

namespace Sonicate.GUI.Views;

public partial class MediaInfoView : UserControl
{
    private ScrollViewer? _trackListViewer;
    public MediaInfoView()
    {
        InitializeComponent();
        this.AttachedToVisualTree += (_, _) =>
        {
            _trackListViewer = this.FindControl<ScrollViewer>("TrackListViewer");
            if (_trackListViewer == null)
            {
                throw new InvalidOperationException("TrackListViewer control not found.");
            }

            if (DataContext is MediaInfoViewModel vm)
            {
                _trackListViewer.ScrollChanged += (_, args) =>
                {
                    vm.ScrollSync.Offset = _trackListViewer.Offset.Y;
                };

                vm.ScrollSync.WhenAnyValue(x => x.Offset)
                    .Where(_ => _trackListViewer != null)
                    .Subscribe(offset =>
                    {
                        _trackListViewer.Offset = new Vector(_trackListViewer.Offset.X, offset);
                    });
            }
        };
    }
}
