using CommunityToolkit.Mvvm.ComponentModel;

namespace Canvas.ViewModels;

/// <summary>
/// Модель представления для канвы.
/// </summary>
public class CanvasVM : ObservableObject
{
    private int _width;

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
    public int ActualHeight { get; set; }
}