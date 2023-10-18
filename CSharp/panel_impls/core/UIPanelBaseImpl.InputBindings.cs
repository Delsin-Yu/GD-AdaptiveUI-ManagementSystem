using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Core;

public partial class UIPanelBaseImpl
{
    private static readonly Queue<RegisteredInputEvent> s_NotifyQueue = new();
    private Action m_RegisteredPanelWiseCancel;
    
    private void OnCancelPressed(InputEvent _) => m_RegisteredPanelWiseCancel?.Invoke();

    private void DisposeRegisteredEvents()
    {
        m_RegisteredInputEvent.Clear();
        m_RegisteredInputEventNames.Clear();
    }
    
    #warning protected void EnablePanelWiseScrollNavigation() => AdpUIPanelManager.EnableScrollNavigation(this, false);
    #warning protected void DisablePanelWiseScrollNavigation() => AdpUIPanelManager.DisableScrollNavigation(this, false);
    #warning protected void EnablePanelWiseScrollHorizontalNavigation() => AdpUIPanelManager.EnableScrollNavigation(this, true);
    #warning protected void DisablePanelWiseScrollHorizontalNavigation() => AdpUIPanelManager.DisableScrollNavigation(this, true);
    
    private readonly List<string> m_RegisteredInputEventNames = new();
    private readonly Dictionary<string, RegisteredInputEvent> m_RegisteredInputEvent = new();
    
    private class RegisteredInputEvent
    {
        private Action<InputEvent> m_PressedCall;
        private Action<InputEvent> m_ReleasedCall;
        private Action<InputEvent> m_AnyCall;

        public bool Empty =>
            m_PressedCall == null &&
            m_ReleasedCall == null &&
            m_AnyCall == null;

        public void RegisterCall(Action<InputEvent> call, InputActionPhase inputActionPhase) => 
            GetCall(inputActionPhase) += call;

        public void RemoveCall(Action<InputEvent> call, InputActionPhase inputActionPhase) => 
            GetCall(inputActionPhase) -= call;

        public void Call(InputEvent inputEvent, InputActionPhase inputActionPhase)
        {
            GetCall(inputActionPhase)?.Invoke(inputEvent);
            m_AnyCall?.Invoke(inputEvent);
        }

        private ref Action<InputEvent> GetCall(InputActionPhase inputActionPhase)
        {
            switch (inputActionPhase)
            {
                case InputActionPhase.Pressed:
                    return ref m_PressedCall;
                case InputActionPhase.Released:
                    return ref m_ReleasedCall;
                case InputActionPhase.Any:
                    return ref m_AnyCall;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputActionPhase), inputActionPhase, null);
            }
        }
    }
    
    public sealed override void _Input(InputEvent inputEvent)
    {
        foreach (var eventName in CollectionsMarshal.AsSpan(m_RegisteredInputEventNames))
        {
            if(!InputMap.ActionHasEvent(eventName, inputEvent)) continue;
            s_NotifyQueue.Enqueue(m_RegisteredInputEvent[eventName]);
        }

        if(s_NotifyQueue.Count == 0) return;

        var currentPhase = inputEvent.IsPressed() ? InputActionPhase.Pressed : InputActionPhase.Released;        
        
        while (s_NotifyQueue.TryDequeue(out var callDictionary))
        {
            try
            {
                callDictionary.Call(inputEvent, currentPhase);
            }
            catch (Exception e)
            {
                AdpUIPanelManager.LogError(
                    $"""
                     ┌┈┈┈┈ Input Error ┈┈┈┈
                     │ {e.GetType().Name} on executing input action
                     │   {inputEvent.AsText()}:
                     │ Message:
                     │   {e.Message}
                     └┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈
                     """
                );
            }
        }
        
        AcceptEvent();
    }
}
