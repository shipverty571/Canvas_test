using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace Canvas.Drawing;

/// <summary>
/// Сервис для отрисовки канвы.
/// </summary>
public class DrawingService
{
    /// <summary>
    /// Канва.
    /// </summary>
    private SKCanvas _canvas;
    
    /// <summary>
    /// Запускает отрисовку канвы.
    /// </summary>
    /// <param name="eventArgs">Объект события при запуске обработчика OnPaintSurface,
    /// хранящий информацию о канве.</param>
    //TODO: Если кроме канвы в будущем в этом классе ничего не понадобится, то не вижу смысла передавать все аргументы события.
    public void Draw(SKPaintSurfaceEventArgs eventArgs)
    {
        _canvas = eventArgs.Surface.Canvas;
        
        DrawGrid();
    }

    /// <summary>
    /// Рисует сетку на канве.
    /// </summary>
    private void DrawGrid()
    {
        ClearCanvas();

        var width = _canvas.DeviceClipBounds.Width;
        var height = _canvas.DeviceClipBounds.Height;
        var countColumns = width / ConstValues.GridSize + 1;
        var countRows = height / ConstValues.GridSize + 1;

        for (int i = 0; i < countColumns; i++)
        {
            _canvas.DrawLine(
                i * ConstValues.GridSize, 
                0, 
                i * ConstValues.GridSize, 
                height,
                new SKPaint { Color = SKColors.Black });
        }
        
        for (int i = 0; i < countRows; i++)
        {
            _canvas.DrawLine(
                0, 
                i * ConstValues.GridSize, 
                width, 
                i * ConstValues.GridSize,
                new SKPaint { Color = SKColors.Black });
        }
    }

    /// <summary>
    /// Очищает канву.
    /// </summary>
    private void ClearCanvas()
    {
        _canvas.Clear(SKColors.White);
    }
}