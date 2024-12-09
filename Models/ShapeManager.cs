using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace belonging.Models
{
    public class ShapeManager
    {
        private readonly Canvas _canvas;
        private readonly List<Shape> _shapes = new List<Shape>();
        private Shape? _draggedShape;
        private Point _dragStart;

        public ShapeManager(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
            _canvas.Children.Add(shape);
        }

        public void ClearShapes()
        {
            _shapes.Clear();
            _canvas.Children.Clear();
        }

        public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var point = e.GetPosition(_canvas);
            _draggedShape = null;

            foreach (var shape in _shapes)
            {
                if (shape.Bounds.Contains(point))
                {
                    _draggedShape = shape;
                    _dragStart = point;
                    break;
                }
            }

            if (_draggedShape == null)
            {
                ShowMessage("Не попал", point);
            }
            else
            {
                ShowMessage("Попадание", point);
            }
        }

        public void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_draggedShape != null)
            {
                var currentPoint = e.GetPosition(_canvas);
                var delta = currentPoint - _dragStart;

                var newLeft = Canvas.GetLeft(_draggedShape) + delta.X;
                var newTop = Canvas.GetTop(_draggedShape) + delta.Y;

                // Проверка границ 
                if (newLeft < 0) newLeft = 0;
                if (newTop < 0) newTop = 0;
                if (newLeft + _draggedShape.Bounds.Width > _canvas.Bounds.Width) newLeft = _canvas.Bounds.Width - _draggedShape.Bounds.Width;
                if (newTop + _draggedShape.Bounds.Height > _canvas.Bounds.Height) newTop = _canvas.Bounds.Height - _draggedShape.Bounds.Height;

                Canvas.SetLeft(_draggedShape, newLeft);
                Canvas.SetTop(_draggedShape, newTop);

                _dragStart = currentPoint;
            }
        }

        public void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _draggedShape = null;
        }

        private async void ShowMessage(string message, Point point)
        {
            var textBlock = new TextBlock
            {
                Text = message,
                FontSize = 50,
                Foreground = Brushes.Red,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                TextAlignment = Avalonia.Media.TextAlignment.Center
            };

            Canvas.SetLeft(textBlock, point.X - 100);
            Canvas.SetTop(textBlock, point.Y - 50);

            _canvas.Children.Add(textBlock);

            await Task.Delay(2000);
            _canvas.Children.Remove(textBlock);
        }
    }
}