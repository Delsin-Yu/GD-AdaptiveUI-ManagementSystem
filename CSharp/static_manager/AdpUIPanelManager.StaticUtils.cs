using DEYU.GDUtilities.AdpUIManagementSystem.Utils;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    private static readonly Godot.Collections.Array s_OneVariableArray = new(new Variant[1]);

    private const string PointerButtonMaker_EnablePointerInteraction_MethodName = "EnablePointerInteraction";
    
    private static void TogglePanel(CanvasItem impl, bool enabled)
    {
        impl.Modulate = enabled ? Colors.White : Colors.Transparent;

        SetNodeChildAvailability(impl, enabled);

        impl.Visible = enabled;
    }
    
    internal static void SetNodeChildAvailability(Node impl, bool enabled)
    {
        s_OneVariableArray[0] = Variant.From(enabled ? Control.FocusModeEnum.All : Control.FocusModeEnum.None);
        impl.PropagateCall(Control.MethodName.SetFocusMode, s_OneVariableArray, true);

        s_OneVariableArray[0] = Variant.From(enabled ? Control.MouseFilterEnum.Stop : Control.MouseFilterEnum.Ignore);
        impl.PropagateCall(Control.MethodName.SetMouseFilter, s_OneVariableArray, true);
        
        s_OneVariableArray[0] = Variant.From(enabled);
        impl.PropagateCall(Node.MethodName.SetPhysicsProcess, s_OneVariableArray, true);
        impl.PropagateCall(Node.MethodName.SetProcessInput, s_OneVariableArray, true);
        impl.PropagateCall(Node.MethodName.SetProcess, s_OneVariableArray, true);
        
        if (enabled) impl.PropagateCall(PointerButtonMaker_EnablePointerInteraction_MethodName, null, true);
    }
}
