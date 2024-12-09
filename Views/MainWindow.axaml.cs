using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using belonging.Models;

namespace belonging.Views;

public partial class MainWindow : Window
{
    private readonly ShapeManager _shapeManager;

    public MainWindow()
    {
        InitializeComponent();
        _shapeManager = new ShapeManager(DrawingCanvas);

        // Установка обработчиков событий в коде позади 
        DrawingCanvas.PointerPressed += _shapeManager.OnPointerPressed;
        DrawingCanvas.PointerMoved += _shapeManager.OnPointerMoved;
        DrawingCanvas.PointerReleased += _shapeManager.OnPointerReleased;
    }

    private void OnDrawSquareClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _shapeManager.ClearShapes();

        var square = new Rectangle
        {
            Width = 100,
            Height = 100,
            Fill = Brushes.Black // Изменение цвета на черный
        };

        Canvas.SetLeft(square, (DrawingCanvas.Bounds.Width - square.Width) / 2);
        Canvas.SetTop(square, (DrawingCanvas.Bounds.Height - square.Height) / 2);

        _shapeManager.AddShape(square);
    }

    private void OnDrawPentagonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _shapeManager.ClearShapes();

        var pentagon = new Polygon
        {
            Points = new Avalonia.Collections.AvaloniaList<Point>
            {
                new Point(50, 0),   // Верхняя вершина 
                new Point(100, 38), // Правая верхняя 
                new Point(81, 100), // Правая нижняя 
                new Point(19, 100), // Левая нижняя 
                new Point(0, 38)    // Левая верхняя 
            },
            Fill = Brushes.Orange // Изменение цвета на оранжевый
        };

        Canvas.SetLeft(pentagon, (DrawingCanvas.Bounds.Width - 100) / 2);
        Canvas.SetTop(pentagon, (DrawingCanvas.Bounds.Height - 100) / 2);

        _shapeManager.AddShape(pentagon);
    }

    private void OnDrawHexagonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _shapeManager.ClearShapes();

        var hexagon = new Polygon
        {
            Points = new Avalonia.Collections.AvaloniaList<Point>
            {
                new Point(50, 0),   // Верхняя центральная 
                new Point(100, 25), // Правая верхняя 
                new Point(100, 75), // Правая нижняя 
                new Point(50, 100), // Нижняя центральная 
                new Point(0, 75),   // Левая нижняя 
                new Point(0, 25)    // Левая верхняя 
            },
            Fill = Brushes.Purple // Изменение цвета на фиолетовый
        };

        Canvas.SetLeft(hexagon, (DrawingCanvas.Bounds.Width - 100) / 2);
        Canvas.SetTop(hexagon, (DrawingCanvas.Bounds.Height - 100) / 2);

        _shapeManager.AddShape(hexagon);
    }
}