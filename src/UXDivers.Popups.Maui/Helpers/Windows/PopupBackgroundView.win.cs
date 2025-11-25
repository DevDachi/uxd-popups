using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Grid = Microsoft.UI.Xaml.Controls.Grid;
using Point = Windows.Foundation.Point;

namespace UXDivers.Popups.Maui;

public partial class PopupBackgroundView : Grid, IDisposable
{
    private readonly FrameworkElement _popupContent;
    private readonly Action _backgroundAction;

    public PopupBackgroundView(
        FrameworkElement popupContent, 
        Action backgroundAction)
    {
        _popupContent = popupContent;
        _backgroundAction = backgroundAction;
        
        IsHitTestVisible = true;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }
    
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        PointerPressed -= OnPointerPressed;
        PointerPressed += OnPointerPressed;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        PointerPressed -= OnPointerPressed;
    }
    
    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var pointerPosition = e.GetCurrentPoint(this).Position;

        // Transform the pointer position to the popup content's coordinate space
        var transform = _popupContent.TransformToVisual(this);
        var popupBounds = new Windows.Foundation.Rect(transform.TransformPoint(new Point()),
            new Windows.Foundation.Size(_popupContent.ActualWidth, _popupContent.ActualHeight));

        // If the pointer is outside the popup content
        if (!popupBounds.Contains(pointerPosition))
        {
            _backgroundAction?.Invoke();
        }
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }
        
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;
        PointerPressed -= OnPointerPressed;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}