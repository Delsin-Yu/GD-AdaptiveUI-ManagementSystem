using System.Collections.Generic;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;
using DEYU.GDUtilities.AdpUIManagementSystem.Utils;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    // private static readonly Godot.Collections.Array s_OneVariableArray = new(new Variant[1]);

    private static void TogglePanel(UIPanelBaseImpl impl, bool enabled)
    {
        impl.Modulate = enabled ? Colors.White : Colors.Transparent;

        SetNodeChildAvailability(impl, enabled);

        impl.Visible = enabled;
    }

    internal static void SetNodeChildAvailability(UIPanelBaseImpl impl, bool enabled)
    {
        ToggleFocusModeRecursive(impl, enabled, impl.CachedNodeFocusMode);
        // s_OneVariableArray[0] = Variant.From(enabled ? Control.FocusModeEnum.All : Control.FocusModeEnum.None);
        // impl.PropagateCall(Control.MethodName.SetFocusMode, s_OneVariableArray, true);
        //
        // s_OneVariableArray[0] = Variant.From(enabled ? Control.MouseFilterEnum.Stop : Control.MouseFilterEnum.Ignore);
        // impl.PropagateCall(Control.MethodName.SetMouseFilter, s_OneVariableArray, true);
        //
        //
        // s_OneVariableArray[0] = Variant.From(enabled);
        // impl.PropagateCall(Node.MethodName.SetPhysicsProcess, s_OneVariableArray, true);
        // impl.PropagateCall(Node.MethodName.SetProcessInput, s_OneVariableArray, true);
        // impl.PropagateCall(Node.MethodName.SetProcess, s_OneVariableArray, true);
        //
        // if (enabled) impl.PropagateCall(PointerButtonMaker_EnablePointerInteraction_MethodName, null, true);
    }

    /// <summary>
    /// Enable or Disables the focus mode for <paramref name="root"/> and its recursive children, <paramref name="cachedNodeFocusMode"/> and the focus mode for the node or any affected child should not be tampered with after calling this method with <paramref name="enable"/> set to false. 
    /// </summary>
    /// <param name="root">Root node of the enumeration</param>
    /// <param name="enable">When set to false, the method sets the focus mode for <paramref name="root"/> and its recursive children to None, and writes their original values into <paramref name="cachedNodeFocusMode"/>, when set to true, the method restore the focus mode for <paramref name="root"/> and its recursive children to the values stored inside <paramref name="cachedNodeFocusMode"/> and clears it.</param>
    /// <param name="includeInternal">If includeInternal is false, the recursive enumeration excludes nodes' internal children (see <c>internal</c> parameter in <see cref="M:Godot.Node.AddChild(Godot.Node,System.Boolean,Godot.Node.InternalMode)" />)</param>
    private static void ToggleFocusModeRecursive(Node root, bool enable, Dictionary<Control, Control.FocusModeEnum> cachedNodeFocusMode, bool includeInternal = false)
    {
        if (!enable)
        {
            cachedNodeFocusMode.Clear();
            DisableFocusModeRecursive(root, cachedNodeFocusMode, includeInternal);
        }
        else
        {
            EnableFocusModeRecursive(root, cachedNodeFocusMode, includeInternal);
            cachedNodeFocusMode.Clear();
        }
    }

    /// <summary>
    /// Restore the focus mode for <paramref name="root"/> and its recursive children to the values stored inside <paramref name="cachedNodeFocusMode"/>
    /// </summary>
    private static void EnableFocusModeRecursive(Node root, Dictionary<Control, Control.FocusModeEnum> cachedNodeFocusMode, bool includeInternal = false)
    {
        if (root is Control control)
        {
            // Only enable if the node is present in the cache
            if (cachedNodeFocusMode.Remove(control, out var cachedFocusMode))
            {
                control.FocusMode = cachedFocusMode;
            }
        }

        foreach (var child in root.GetChildren(includeInternal))
        {
            if (child is UIPanelBaseImpl) continue;
            EnableFocusModeRecursive(child, cachedNodeFocusMode, includeInternal);
        }
    }

    /// <summary>
    /// Set the focus mode for <paramref name="root"/> and its recursive children to None, and write their original values into <paramref name="cachedNodeFocusMode"/>   
    /// </summary>
    private static void DisableFocusModeRecursive(Node root, Dictionary<Control, Control.FocusModeEnum> cachedNodeFocusMode, bool includeInternal = false)
    {
        if (root is Control control)
        {
            var controlFocusMode = control.FocusMode;
            // Only cache the control when it is in any form of focusable
            if (controlFocusMode != Control.FocusModeEnum.None)
            {
                cachedNodeFocusMode[control] = controlFocusMode;
                control.FocusMode = Control.FocusModeEnum.None;
            }
        }

        foreach (var child in root.GetChildren(includeInternal))
        {
            if (child is UIPanelBaseImpl) continue;
            DisableFocusModeRecursive(child, cachedNodeFocusMode, includeInternal);
        }
    }
}
