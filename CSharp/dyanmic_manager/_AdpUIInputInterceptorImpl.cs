using System;
using System.Linq;
using Godot;
using Godot.Collections;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

/// <summary>
/// Input Interceptor the framework uses for blocking input events from non active input schemes
/// </summary>
public abstract partial class _AdpUIInputInterceptorImpl : Node
{
    private uint? m_LastRequestedInputMode;
    private SchemeLayerData[] m_SchemeLayerData;
    private Viewport m_Viewport;

    [Export] public string CancelActionName { get; private set; } = "ui_cancel";

    [Export]
    private Array<string[]> InputSchemeLayerData { get; set; } = new(
        new[]
        {
            AdpUIInputScheme.BuiltinUIActionStrings.ToArray()
        }
    );

    private class SchemeLayerData
    {
        private readonly string[] m_LayerActions;
        private readonly InputEventMouse[] m_MouseEvents;

        public SchemeLayerData(string[] layerActions)
        {
            m_LayerActions = layerActions;
            m_MouseEvents =
                m_LayerActions
                   .SelectMany(
                        actionName =>
                            InputMap
                               .ActionGetEvents(actionName)
                               .Where(inputEvent => inputEvent is InputEventMouse)
                               .Cast<InputEventMouse>()
                    )
                   .ToArray();
        }

        public bool HasEvent(InputEvent inputEvent)
        {
            var span = m_LayerActions.AsSpan();
            foreach (var inputActionName in span)
            {
                if (InputMap.ActionHasEvent(inputActionName, inputEvent)) return true;
            }

            return false;
        }

        public bool HasMouseEvent(InputEventMouse inputEventMouse)
        {
            var span = m_MouseEvents.AsSpan();
            foreach (var mouseInputEventCandidate in span)
            {
                if (mouseInputEventCandidate.IsMatch(inputEventMouse)) return true;
            }

            return false;
        }
    }

    private Viewport GetCurrentViewport() => m_Viewport ??= GetViewport();

    internal void Load() => m_SchemeLayerData = InputSchemeLayerData.Select(layerActions => new SchemeLayerData(layerActions)).ToArray();

    internal void UpdateInputScheme(uint requestedInputScheme)
    {
        if (m_LastRequestedInputMode == requestedInputScheme) return;
        if (m_SchemeLayerData.Length <= requestedInputScheme) AdpUIPanelManager.LogError($"The requested input scheme ({requestedInputScheme}) is not present");
        m_LastRequestedInputMode = requestedInputScheme;
    }

    public sealed override void _Input(InputEvent inputEvent)
    {
        if (!IsInsideTree() || m_LastRequestedInputMode == null || m_SchemeLayerData.Length <= m_LastRequestedInputMode) return;

        var schemeLayerDataSpan = m_SchemeLayerData.AsSpan();
        if (schemeLayerDataSpan[(int)m_LastRequestedInputMode.Value].HasEvent(inputEvent)) return;

        if (inputEvent is InputEventMouse inputEventMouse && !RequireInterceptMouseEvent(m_LastRequestedInputMode.Value, inputEventMouse, schemeLayerDataSpan))
        {
            return;
        }

        GetCurrentViewport().SetInputAsHandled();
    }

    private static bool RequireInterceptMouseEvent(uint skipIndex, InputEventMouse inputEventMouse, in ReadOnlySpan<SchemeLayerData> schemeLayerData)
    {
        for (var index = 0; index < schemeLayerData.Length; index++)
        {
            if (index == skipIndex) continue;
            if (schemeLayerData[index].HasMouseEvent(inputEventMouse)) return true;
        }

        return false;
    }
}
