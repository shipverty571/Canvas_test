using CommunityToolkit.Mvvm.ComponentModel;

namespace Canvas.ViewModels;

/// <summary>
/// Модель представления для канвы.
/// </summary>
public class CanvasVM : ObservableObject
{
    /// <summary>
    /// Ширина канвы.
    /// </summary>
    private int _width;

    /// <summary>
    /// Высота канвы.
    /// </summary>
    private int _height;

    /// <summary>
    /// Актуальная ширина канвы.
    /// </summary>
    public int ActualWidth
    {
        get => _width;
        set
        {
            _width = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Актуальная высота канвы.
    /// </summary>
    public int ActualHeight
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged();
        }
    }
}