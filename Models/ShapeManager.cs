﻿using Avalonia;
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
        private TextBlock? _currentMessage; // Текущее сообщение

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
                    ShowMessage("Попадание");
                    break;
                }
            }

            if (_draggedShape == null)
            {
                ShowMessage("Не попал");
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

        private async void ShowMessage(string message)
        {
            // Удаляем предыдущее сообщение, если оно есть
            if (_currentMessage != null)
            {
                _canvas.Children.Remove(_currentMessage);
                _currentMessage = null;
            }

            // Создаем новое сообщение
            _currentMessage = new TextBlock
            {
                Text = message,
                FontSize = 50,
                Foreground = Brushes.Red,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top,
                TextAlignment = Avalonia.Media.TextAlignment.Center
            };

            Canvas.SetLeft(_currentMessage, (_canvas.Bounds.Width - 200) / 2); // Центрирование по горизонтали
            Canvas.SetTop(_currentMessage, 50); // Позиция сверху

            _canvas.Children.Add(_currentMessage);

            // Удаляем сообщение через 2 секунды
            await Task.Delay(2000);
            if (_currentMessage != null)
            {
                _canvas.Children.Remove(_currentMessage);
                _currentMessage = null;
            }
        }
    }
}