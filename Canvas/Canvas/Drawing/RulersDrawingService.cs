using SkiaSharp;

namespace Canvas.Drawing;

/// <summary>
/// Класс для отрисовки линейки.
/// </summary>
public class RulersDrawingService
{
    /// <summary>
    /// Начальная координата отрисовки маленькой линии по оси.
    /// </summary>
    private const int StartCoordinateSmallLine = 20;
    
    /// <summary>
    /// Конечная координата отрисовки маленькой линии по оси.
    /// </summary>
    private const int EndCoordinateSmallLine = 30;
    
    /// <summary>
    /// Начальная координата отрисовки большой линии по оси.
    /// </summary>
    private const int StartCoordinateBigLine = 10;
    
    /// <summary>
    /// Конечная координата отрисовки большой линии по оси.
    /// </summary>
    private const int EndCoordinateBigLine = 30;

    /// <summary>
    /// Значение смещения текста от начальной точки.
    /// </summary>
    private const int StartTextShift = 15;

    /// <summary>
    /// Значение смещения текста от крайней правой стороны.
    /// </summary>
    private const int EndTextShift = 5;

    /// <summary>
    /// Вспомогательная координата для отрисовки текста.
    /// </summary>
    private const int CoordinateText = 25;

    /// <summary>
    /// Размер текста.
    /// </summary>
    private const int FontSize = 15;
    
    /// <summary>
    /// Канва.
    /// </summary>
    private SKCanvas _canvas;

    /// <summary>
    /// Создаёт экземпляр класса <see cref="RulersOrientations"/>.
    /// </summary>
    /// <param name="canvas">Канва линейки.</param>
    public RulersDrawingService(SKCanvas canvas)
    {
        _canvas = canvas;
    }

    /// <summary>
    /// Отрисовывает линейку в зависимости от её ориентации и размера.
    /// </summary>
    /// <param name="orientation">Ориентация линейки.</param>
    /// <param name="size">Размер линейки.</param>
    public void DrawRuler(RulersOrientations orientation, int size)
    {
        ClearCanvas();
        
        var countLines = size / ConstValues.GridSize + 1;

        for (int i = 0; i < countLines; i++)
        {
            if (i % 5 != 0)
            {
                DrawSmallLine(i, orientation);
            }
            else
            {
                DrawBigLine(i, orientation);
                DrawText(i, orientation);
            }
        }
    }
    
    /// <summary>
    /// Очищает канву.
    /// </summary>
    private void ClearCanvas()
    {
        _canvas.Clear(SKColors.LightGray);
    }
    
    /// <summary>
    /// Отрисовывает маленькую линию на линейке.
    /// </summary>
    /// <param name="number">Номер линии.</param>
    /// <param name="orientation">Ориентация линейки.</param>
    private void DrawSmallLine(int number, RulersOrientations orientation)
    {
        var paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 1,
            Color = SKColors.Red
        };

        switch (orientation)
        {
            case RulersOrientations.Horizontal:
            {
                _canvas.DrawLine(
                    number * ConstValues.GridSize,
                    StartCoordinateSmallLine,
                    number * ConstValues.GridSize,
                    EndCoordinateSmallLine,
                    paint);
                break;
            }
            case RulersOrientations.Vertical:
            {
                _canvas.DrawLine(
                    StartCoordinateSmallLine,
                    number * ConstValues.GridSize,
                    EndCoordinateSmallLine,
                    number * ConstValues.GridSize,
                    paint);
                break;
            }
        }
        
    }

    /// <summary>
    /// Отрисовывает большую линию на линейке.
    /// </summary>
    /// <param name="number">Номер линии.</param>
    /// <param name="orientation">Ориентация линейки.</param>
    private void DrawBigLine(int number, RulersOrientations orientation)
    {
        var paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2,
            Color = SKColors.Red
        };

        switch (orientation)
        {
            case RulersOrientations.Horizontal:
            {
                _canvas.DrawLine(
                    number * ConstValues.GridSize,
                    StartCoordinateBigLine,
                    number * ConstValues.GridSize,
                    EndCoordinateBigLine,
                    paint);
                break;
            }
            case RulersOrientations.Vertical:
            {
                _canvas.DrawLine(
                    StartCoordinateBigLine,
                    number * ConstValues.GridSize,
                    EndCoordinateBigLine,
                    number * ConstValues.GridSize,
                    paint);
                break;
            }
        }
    }

    /// <summary>
    /// Отрисовывает текст на линейке рядом с большой линией.
    /// </summary>
    /// <param name="number">Номер линии.</param>
    /// <param name="orientation">Ориентация линейки.</param>
    private void DrawText(int number, RulersOrientations orientation)
    {
        var font = new SKFont
        {
            Size = FontSize
        };
        
        switch (orientation)
        {
            case RulersOrientations.Horizontal:
            {
                var path = new SKPath();
                path.MoveTo(
                    number * ConstValues.GridSize - EndTextShift - StartTextShift, 
                    CoordinateText);
                path.LineTo(
                    number * ConstValues.GridSize - EndTextShift, 
                    CoordinateText);
                
                var paint = new SKPaint()
                {
                    TextAlign = SKTextAlign.Right
                };
                
                _canvas.DrawTextOnPath(
                    number.ToString(), 
                    path, 
                    new SKPoint(0, 0), 
                    false, 
                    font, 
                    paint);
                break;
            }
            case RulersOrientations.Vertical:
            {
                var path = new SKPath();
                path.MoveTo(
                    CoordinateText, 
                    number * ConstValues.GridSize - EndTextShift);
                path.LineTo(
                    CoordinateText, 
                    number * ConstValues.GridSize - EndTextShift - StartTextShift);
                
                var paint = new SKPaint()
                {
                    TextAlign = SKTextAlign.Left
                };
                
                _canvas.DrawTextOnPath(
                    number.ToString(), 
                    path, 
                    new SKPoint(0, 0), 
                    false, 
                    font, 
                    paint);
                break;
            }
        }
    }
}