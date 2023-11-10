using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

/// <summary>
/// Data containers consumed when framework is initializing
/// </summary>
public partial class _AdpUILoaderImpl : CanvasLayer
{
    [Export] internal _AdpUIAudioInterfaceImpl AudioInterfaceImpl { get; private set; }
    [Export] internal _AdpUIInputInterceptorImpl InputInterceptorImpl { get; private set; }
    [Export] internal Control DefaultPanelRoot { get; private set; }

    public sealed override void _Ready()
    {
        base._Ready();
        AdpUIPanelManager.RegisterLoader(this);
    }
}
