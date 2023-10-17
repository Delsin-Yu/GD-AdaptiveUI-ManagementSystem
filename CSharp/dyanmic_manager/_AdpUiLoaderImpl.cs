using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public partial class _AdpUiLoaderImpl : Node
{
    [Export] internal _AdpUiAudioInterfaceImpl AudioInterfaceImpl { get; private set; }
    [Export] internal _AdpUiInputInterceptorImpl InputInterceptorImpl { get; private set; }
    [Export] internal Control DefaultPanelRoot { get; private set; }
}
