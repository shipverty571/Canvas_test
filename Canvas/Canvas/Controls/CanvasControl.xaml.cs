using System.Windows.Controls;
using Canvas.Drawing;
using Canvas.ViewModels;
using SkiaSharp.Views.Desktop;

namespace Canvas.Controls;

/// <summary>
/// Контрол для канвы.
/// </summary>
public partial class CanvasControl : UserControl
{
    /// <summary>
    /// Сервис отрисовки канвы.
    /// </summary>
    private DrawingService _drawingService;
    
    public CanvasControl()
    {
        InitializeComponent();

        var vm = new CanvasVM();
        DataContext = vm;

        _drawingService = new DrawingService();
        
        // Запускает в отдельном процессе ререндеринг канвы.
        // При запуске нового рендеринга вызывается обработчик CanvasElement_OnPaintSurface.
        _ = Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        CanvasElement.InvalidateVisual();
                    });
                    // Канва ререндерится каждую одну миллисекунду.
                    // TODO: протестить производительность на большом количестве элементов
                    _ = SpinWait.SpinUntil(() => false, 1);
                }
                catch
                {
                    break;
                }
            }
        });
    }

    private void CanvasElement_OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        _drawingService.Draw(e);
    }
}