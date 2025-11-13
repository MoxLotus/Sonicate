using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Controls.Primitives;
using Sonicate.GUI.ViewModels;

namespace Sonicate.GUI.Views;

// A small IDataTemplate-based selector that delegates to templates provided as properties.
public class TrackTemplateSelector : IDataTemplate, ITemplate<object?, Control?>
{
    public IDataTemplate? AudioTemplate { get; set; }
    public IDataTemplate? VideoTemplate { get; set; }
    public IDataTemplate? DefaultTemplate { get; set; }

    // Always match — ItemsControl will call Build for each item.
    public bool Match(object? data) => true;

    // Explicit implementation for ITemplate<object?, Control?>
    Control? ITemplate<object?, Control?>.Build(object? data)
    {
        if (data is AudioTrackInfoVM && AudioTemplate is not null)
            return AudioTemplate.Build(data);

        if (data is VideoTrackInfoVM && VideoTemplate is not null)
            return VideoTemplate.Build(data);

        if (DefaultTemplate is not null)
            return DefaultTemplate.Build(data);

        // Fallback UI so we never return null
        return new TextBlock { Text = data?.ToString() ?? string.Empty };
    }
}