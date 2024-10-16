using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Canvas.Controls;

public partial class TopCanvasRulerControl : UserControl
{
    public static readonly DependencyProperty ActualCanvasWidthProperty =
        DependencyProperty.Register(
            "ActualCanvasWidth",
            typeof(int),
            typeof(TopCanvasRulerControl),
            new FrameworkPropertyMetadata
            {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
    
    public static readonly DependencyProperty ActualCanvasHeightProperty = DependencyProperty.Register(
        nameof(ActualCanvasHeight), 
        typeof(int),
        typeof(TopCanvasRulerControl));
    
    public TopCanvasRulerControl()
    {
        InitializeComponent();
    }

    public int ActualCanvasWidth
    {
        get => (int)GetValue(ActualCanvasWidthProperty);
        set => SetValue(ActualCanvasWidthProperty, value);
    }
    
    public int ActualCanvasHeight
    {
        get => (int)GetValue(ActualCanvasHeightProperty);
        set
        {
            Debug.WriteLine(value);
            SetValue(ActualCanvasHeightProperty, value);
        }
    }
}