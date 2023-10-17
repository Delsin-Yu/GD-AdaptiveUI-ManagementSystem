using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public abstract partial class _AdpUiInputInterceptorImpl : Node
{
    private uint? m_LastRequestedInputMode;
    private string[][] m_SchemeLayerData;
    private Viewport m_Viewport;


    [Export] public string CancelActionName { get; private set; } = "ui_cancel";

    [Export]
    private Array<string[]> InputSchemeLayerData { get; set; } = new(
        new[]
        {
            AdpUiInputScheme.s_BuiltinUiActionStrings
        }
    );

    private Viewport GetCurrentViewport() => m_Viewport ??= GetViewport();

    internal void Load() => m_SchemeLayerData = InputSchemeLayerData.Select(x => x.ToArray()).ToArray();

    internal void UpdateInputScheme(uint requestedInputScheme)
    {
        if (m_LastRequestedInputMode == requestedInputScheme) return;
        if (m_SchemeLayerData.Length <= requestedInputScheme) AdpUiPanelManager.LogError($"The requested input scheme ({requestedInputScheme}) is not present");
        m_LastRequestedInputMode = requestedInputScheme;
    }

    public sealed override void _Input(InputEvent inputEvent)
    {
        if (!IsInsideTree() || m_LastRequestedInputMode == null || m_SchemeLayerData.Length <= m_LastRequestedInputMode) return;

        var currentInputLayer = m_SchemeLayerData[m_LastRequestedInputMode.Value].AsSpan();
        foreach (var name in currentInputLayer)
        {
            if (InputMap.ActionHasEvent(name, inputEvent)) return;
        }

        GetCurrentViewport().SetInputAsHandled();
    }
}
