using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Canvas.Drawing;
using SkiaSharp.Views.Desktop;

namespace Canvas.Controls;

/// <summary>
/// Контрол горизонтальной линейки.
/// </summary>
public partial class HorizontalCanvasRulerControl : UserControl
{
    /// <summary>
    /// Регистрирует свойство зависимости актуальной ширины канвы.
    /// </summary>
    public static readonly DependencyProperty ActualCanvasWidthProperty =
        DependencyProperty.Register(
            nameof(ActualCanvasWidth),
            typeof(int),
            typeof(HorizontalCanvasRulerControl),
            new FrameworkPropertyMetadata
            {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                PropertyChangedCallback = WidthChanged
            });

    /// <summary>
    /// Сервис отрисовки линейки.
    /// </summary>
    private RulersDrawingService _rulersDrawingService;
    
    public HorizontalCanvasRulerControl()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает и задает актуальную ширину канвы.
    /// </summary>
    public int ActualCanvasWidth
    {
        get => (int)GetValue(ActualCanvasWidthProperty);
        set => SetValue(ActualCanvasWidthProperty, value);
    }

    /// <summary>
    /// Вызывается при изменении свойства <see cref="ActualCanvasWidth"/>.
    /// </summary>
    /// <param name="d">Текущий объект зависимости.</param>
    /// <param name="e">Агрументы события.</param>
    private static void WidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var topRulerControl = d as HorizontalCanvasRulerControl;
        topRulerControl?.OnWidthChanged();
    }

    /// <summary>
    /// При изменении актуальной ширины канвы запускает ререндеринг линейки.
    /// </summary>
    private void OnWidthChanged()
    {
        Dispatcher.Invoke(() =>
        {
            HorizontalRuler.InvalidateVisual();
        });
    }
    
    private void HorizontalRuler_OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        _rulersDrawingService = new RulersDrawingService(e.Surface.Canvas);
        _rulersDrawingService.DrawRuler(RulersOrientations.Horizontal, ActualCanvasWidth);
    }
}