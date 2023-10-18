using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public partial class _AdpUILoaderImpl : Node
{
    [Export] internal _AdpUIAudioInterfaceImpl AudioInterfaceImpl { get; private set; }
    [Export] internal _AdpUIInputInterceptorImpl InputInterceptorImpl { get; private set; }
    [Export] internal Control DefaultPanelRoot { get; private set; }
}
