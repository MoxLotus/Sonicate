using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.Views;

public partial class FileSelectView : UserControl
{
    public FileSelectView()
    {
        InitializeComponent();
    }
    private Point _lastPoint;
    private bool _isDragging = false;

    private void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        _lastPoint = e.GetPosition(FileListBackground);
        _isDragging = true;
        FileListBackground.Cursor = new Cursor(StandardCursorType.Hand);
        e.Handled = true;
    }

    private void OnPointerMoved(object sender, PointerEventArgs e)
    {
        if (_isDragging)
        {
            var currentPoint = e.GetPosition(FileListBackground);
            var deltaX = currentPoint.X - _lastPoint.X;

            FileListViewer.Offset = FileListViewer.Offset.WithX(
                FileListViewer.Offset.X - deltaX);

            _lastPoint = currentPoint;
            e.Handled = true;
        }
    }

    private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
        FileListBackground.Cursor = new Cursor(StandardCursorType.Arrow);
        e.Handled = true;
    }
}
