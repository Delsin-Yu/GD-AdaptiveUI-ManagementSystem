using System;
using System.Threading;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Core;

public abstract partial class UiPanelBaseImpl
{
#region Panel Activities

    public bool IsPanelOpened { get; private set; }
    public CancellationToken OnPanelCloseFadeFinishToken => PanelCloseFadeFinishTokenSource?.Token ?? CancellationToken.None;
    public CancellationToken OnPanelCloseToken => PanelCloseTokenSource?.Token ?? CancellationToken.None;

    public virtual uint RequestedInputScheme => AdpUiInputScheme.UiInputScheme;
    protected virtual void OnPanelInitialize() { }
    protected abstract void OnPanelOpen();
    protected virtual void OnPanelClose() { }
    protected virtual void OnPanelDestroyed() { }

    internal void ClosePanelInternal()
    {
        ClosePanelImpl();
        m_OnPanelCloseCallback?.Invoke(this);
        m_OnPanelCloseCallback = null;
    }

#endregion

#region Panel Visual

    public bool IsPanelShown { get; private set; }
    protected virtual void OnPanelFadeStart(FadeType fadeType, bool isOpenClose) { }
    protected virtual void OnPanelFadeFinish(FadeType fadeType, bool isOpenClose) { }
    public void HidePanel() => TweenOff(false, null);
    public void ShowPanel() => TweenOn(false);

    public virtual bool MutePanelOpenAudio => false;
    public virtual bool MutePanelCloseAudio => false;
    public virtual float GetPanelTransitionDuration() => AdpUiPanelManager.PanelTransitionDuration;

#endregion

#region Input Bindings

    protected void RegisterPanelWiseInput(string inputName, Action<InputEvent> callback, InputActionPhase actionPhase = InputActionPhase.Pressed)
    {
        if (!m_RegisteredInputEvent.TryGetValue(inputName, out var registeredInputEvent))
        {
            registeredInputEvent = new();
            m_RegisteredInputEvent.Add(inputName, registeredInputEvent);
            m_RegisteredInputEventNames.Add(inputName);
        }

        registeredInputEvent.RegisterCall(callback, actionPhase);
    }

    protected void RegisterPanelWiseCancel(Action callback)
    {
        if (m_RegisteredPanelWiseCancel != null)
        {
            m_RegisteredPanelWiseCancel += callback;
        }
        else
        {
            m_RegisteredPanelWiseCancel = callback;
            RegisterPanelWiseInput(AdpUiPanelManager.GetCancelActionName(), OnCancelPressed);
        }
    }

    protected void RemovePanelWiseInput(string inputName, Action<InputEvent> callback, InputActionPhase actionPhase = InputActionPhase.Pressed)
    {
        if (!m_RegisteredInputEvent.TryGetValue(inputName, out var registeredInputEvent)) return;
        registeredInputEvent.RemoveCall(callback, actionPhase);
        if (!registeredInputEvent.Empty) return;
        m_RegisteredInputEvent.Remove(inputName);
        m_RegisteredInputEventNames.Remove(inputName);
    }

    protected void RemovePanelWiseCancel(Action callback)
    {
        m_RegisteredPanelWiseCancel -= callback;
        if (m_RegisteredPanelWiseCancel != null) return;
        RemovePanelWiseInput(AdpUiPanelManager.GetCancelActionName(), OnCancelPressed);
    }

    protected void SetupPanelWiseInput(bool doReg, string inputName, Action<InputEvent> callback)
    {
        if (doReg) RegisterPanelWiseInput(inputName, callback);
        else RemovePanelWiseInput(inputName, callback);
    }

    protected void SetupPanelWiseCancel(bool doReg, Action callback)
    {
        if (doReg) RegisterPanelWiseCancel(callback);
        else RemovePanelWiseCancel(callback);
    }

#endregion
}
