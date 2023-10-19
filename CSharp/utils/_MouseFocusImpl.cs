using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Utils;

public partial class _MouseFocusImpl : Button
{
    public sealed override void _Ready() => 
        MouseEntered += OnMouseEntered;

    protected virtual void OnMouseEntered() => GrabFocus();
}
