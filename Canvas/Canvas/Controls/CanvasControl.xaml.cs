using System.Windows.Controls;
using Canvas.Drawing;
using Canvas.ViewModels;
using SkiaSharp.Views.Desktop;

namespace Canvas.Controls;

public partial class CanvasControl : UserControl
{
    public CanvasControl()
    {
        InitializeComponent();

        var vm = new CanvasVM();
        DataContext = vm;
        
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

    public DrawingService DrawingService { get; set; } = new DrawingService();

    private void CanvasElement_OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        DrawingService.Draw(e);
    }
}