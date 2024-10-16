using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Canvas.Drawing;
using SkiaSharp.Views.Desktop;

namespace Canvas.Controls;

public partial class VerticalCanvasRulerControl : UserControl
{
    /// <summary>
    /// Регистрирует свойство зависимости актуальной высоты канвы.
    /// </summary>
    public static readonly DependencyProperty ActualCanvasHeightProperty =
        DependencyProperty.Register(
            nameof(ActualCanvasHeight),
            typeof(int),
            typeof(VerticalCanvasRulerControl),
            new FrameworkPropertyMetadata
            {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                PropertyChangedCallback = HeightChanged
            });

    /// <summary>
    /// Сервис отрисовки линейки.
    /// </summary>
    private RulersDrawingService _rulersDrawingService;
    
    public VerticalCanvasRulerControl()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Возвращает и задает актуальную высоту канвы.
    /// </summary>
    public int ActualCanvasHeight
    {
        get => (int)GetValue(ActualCanvasHeightProperty);
        set => SetValue(ActualCanvasHeightProperty, value);
    }

    /// <summary>
    /// Вызывается при изменении свойства <see cref="ActualCanvasHeight"/>.
    /// </summary>
    /// <param name="d">Текущий объект зависимости.</param>
    /// <param name="e">Агрументы события.</param>
    private static void HeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var topRulerControl = d as VerticalCanvasRulerControl;
        topRulerControl?.OnHeightChanged();
    }

    /// <summary>
    /// При изменении актуальной высоты канвы запускает ререндеринг линейки.
    /// </summary>
    private void OnHeightChanged()
    {
        Dispatcher.Invoke(() =>
        {
            VerticalRuler.InvalidateVisual();
        });
    }

    private void VerticalRuler_OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        _rulersDrawingService = new RulersDrawingService(e.Surface.Canvas);
        _rulersDrawingService.DrawRuler(RulersOrientations.Vertical, ActualCanvasHeight);
    }
}