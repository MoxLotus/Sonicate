using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace Sonicate.GUI.Views;

public partial class FileSelectView : UserControl
{
    public FileSelectView()
    {
        InitializeComponent();
    }

    // Removes the need for a scrollbar. Provided by Microsoft Copilot.
    #region Dragging

    private Point _previous;
    private bool _isDragging = false;

    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        _previous = e.GetPosition(FileListBackground);
        _isDragging = true;
        FileListBackground.Cursor = new Cursor(StandardCursorType.Hand);
        e.Handled = true;
    }

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (!_isDragging) return;

        Point current = e.GetPosition(FileListBackground);
        double dx = current.X - _previous.X;

        FileListViewer.Offset = FileListViewer.Offset.WithX(
            FileListViewer.Offset.X - dx
        );

        _previous = current;
        e.Handled = true;
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
        FileListBackground.Cursor = new Cursor(StandardCursorType.Arrow);
        e.Handled = true;
    }

    #endregion
}
