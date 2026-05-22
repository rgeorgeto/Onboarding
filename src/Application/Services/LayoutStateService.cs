namespace Application.Services;

public class LayoutStateService
{
    public event Action? OnChange;
    private bool _isSidebarOpen = true;
    public bool IsSidebarOpen
    {
        get => _isSidebarOpen;
        set
        {
            if (_isSidebarOpen != value)
            {
                _isSidebarOpen = value;
                NotifyStateChanged();
            }
        }
    }
    public void Toggle() => IsSidebarOpen = !IsSidebarOpen;
    private void NotifyStateChanged() => OnChange?.Invoke();
}
