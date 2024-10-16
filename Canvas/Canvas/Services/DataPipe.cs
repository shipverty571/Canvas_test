using System.Windows;

namespace Canvas.Services;

/// <summary>
/// Класс для передачи значений свойств контрола в модель представления.
/// </summary>
/// <remarks>
/// Возможно, решение временное, так как я предполагаю, что с появлением фичи с масштабированием канвы
/// придется получать размеры по-другому.
/// </remarks>
public static class DataPipe
{
    /// <summary>
    /// Свойство прослушивания.
    /// </summary>
    public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
        "Observe",
        typeof(bool),
        typeof(DataPipe),
        new FrameworkPropertyMetadata(OnObserveChanged));

    /// <summary>
    /// Свойство для прослушивания ширины.
    /// </summary>
    public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
        "ObservedWidth",
        typeof(double),
        typeof(DataPipe));

    /// <summary>
    /// Свойство для прослушивания высоты.
    /// </summary>
    public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
        "ObservedHeight",
        typeof(double),
        typeof(DataPipe));

    /// <summary>
    /// Используется для получения значения прослушивания свойств контрола.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <returns>Возвращает True или False в зависимости от того, должен ли элемент прослушиваться или нет.</returns>
    public static bool GetObserve(FrameworkElement frameworkElement)
    {
        return (bool)frameworkElement.GetValue(ObserveProperty);
    }

    /// <summary>
    /// Устанавливает значение для получения свойств при прослушивании элемента.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <param name="observe">Значение, которое нужно установить.</param>
    public static void SetObserve(FrameworkElement frameworkElement, bool observe)
    {
        frameworkElement.SetValue(ObserveProperty, observe);
    }

    /// <summary>
    /// Используется для получения актуального значения ширины элемента.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <returns>Возвращает актуальную ширину элемента.</returns>
    public static double GetObservedWidth(FrameworkElement frameworkElement)
    {
        return (double)frameworkElement.GetValue(ObservedWidthProperty);
    }

    /// <summary>
    /// Используется для установки актуального значения ширины элемента.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <param name="observedWidth">Актуалньая ширина элемента.</param>
    public static void SetObservedWidth(FrameworkElement frameworkElement, double observedWidth)
    {
        frameworkElement.SetValue(ObservedWidthProperty, observedWidth);
    }

    /// <summary>
    /// Используется для получения актуального значения высоты элемента.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <returns>Возвращает актуальную высоту элемента.</returns>
    public static double GetObservedHeight(FrameworkElement frameworkElement)
    {
        return (double)frameworkElement.GetValue(ObservedHeightProperty);
    }

    /// <summary>
    /// Используется для установки актуального значения высоты элемента.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    /// <param name="observedHeight">Актуалньая высота элемента.</param>
    public static void SetObservedHeight(FrameworkElement frameworkElement, double observedHeight)
    {
        frameworkElement.SetValue(ObservedHeightProperty, observedHeight);
    }

    /// <summary>
    /// Срабатывает при изменении свойств элемента.
    /// </summary>
    /// <param name="dependencyObject">Объект зависимости.</param>
    /// <param name="e">Аргументы события изменения свойств зависимостей.</param>
    private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var frameworkElement = (FrameworkElement)dependencyObject;

        if ((bool)e.NewValue)
        {
            frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
            UpdateObservedSizesForFrameworkElement(frameworkElement);
        }
        else
        {
            frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
        }
    }

    /// <summary>
    /// Обработчик изменения размеров элемента.
    /// </summary>
    /// <param name="sender">Элемент, вызвавший событие.</param>
    /// <param name="e">Аргументы события.</param>
    private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
    }

    /// <summary>
    /// Обновляет самописные свойства зависимости актуальными данными.
    /// </summary>
    /// <param name="frameworkElement">Элемент представления.</param>
    private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement)
    {
        frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
        frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);
    }
}