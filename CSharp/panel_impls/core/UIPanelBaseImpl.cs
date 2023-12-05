using System;
using System.Collections.Generic;
using System.Threading;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Core;

public abstract partial class UIPanelBaseImpl : Control
{
    private readonly Control[] m_OneLengthSelfArray = new Control[1];
    private Node m_ActiveOnlyVisualElementsTweenKey;

    private PanelLayer m_CurrentPanelLayer;
    private bool m_Initialized;

    private bool m_IsHiddenAtStart;
    private LayerVisual m_LastLayerVisual;
    private Action<UIPanelBaseImpl> m_OnPanelCloseCallback;
    private bool m_ReleasingBufferedPanel;

    [Export] private AudioStream OverrideOnPanelOpenAudio { get; set; }
    [Export] private AudioStream OverrideOnPanelCloseAudio { get; set; }
    [Export] private Control[] ActiveOnlyVisualElements { get; set; }

    private CancellationTokenSource PanelCloseFadeFinishTokenSource { get; set; }
    private CancellationTokenSource PanelCloseTokenSource { get; set; }
    private Control BufferedSelection { get; set; }
    internal Dictionary<Control, AdpUIPanelManager.CachedControlInteractableInfo> CachedNodeFocusMode { get; } = new();

    public sealed override void _Ready() { }

    public sealed override void _Notification(int notificationCode)
    {
        if (notificationCode != NotificationPredelete) return;

        if (!IsTempPanel && !ReleasingBufferedPanel)
            throw new InvalidOperationException(
                $"""
                 一个非临时面板({Name})被意外删除！
                 =====Scene Path=====
                 {GetPath()}
                 =====Scene Path=====
                 """
            );

        OnPanelDestroyed_Protected();
    }

    private void InvokeOnPanelFadeFinish(FadeType fadeType, bool isOpenClose, bool simulated)
    {
        OnPanelFadeFinish_Protected(fadeType, isOpenClose);
        if (isOpenClose && !simulated) OnPanelOpenCloseFadeFinishCallbackInternal?.Invoke(fadeType);
    }

    private void ClosePanelImpl()
    {
        ClosePanelImplShared();
        TweenOff(true, PanelCloseFadeFinishTokenSource);
        PanelCloseFadeFinishTokenSource = new();
        if (!MutePanelCloseAudio_Protected)
        {
            if (OverrideOnPanelCloseAudio != null) AdpUIPanelManager.PlayAudio(OverrideOnPanelCloseAudio);
            else AdpUIPanelManager.PlayDefaultPanelCloseAudio();
        }

        OnPanelClose_Protected();
        AdpUIPanelManager.HandlePanelClose(this, m_CurrentPanelLayer, m_LastLayerVisual);
    }
    
    private void ClosePanelImplShared()
    {
        PanelCloseTokenSource.Cancel();
        PanelCloseTokenSource = new();
        //Debug.LogError($"Closing Panel: {name}", PanelParent);
        if (!IsPanelOpened)
            throw new InvalidOperationException(
                $"""
                 禁止多次关闭已经被关闭的面板({Name})！
                 =====Scene Path=====
                 {GetPath()}
                 =====Scene Path=====
                 """
            );
        IsPanelOpened = false;
    }

    private void TweenOn(bool isOpenClose)
    {
        IsPanelShown = true;
        OnPanelFadeStart_Protected(FadeType.FadeIn, isOpenClose);
        Tween(
            this,
            true,
            m_OneLengthSelfArray,
            () => InvokeOnPanelFadeFinish(FadeType.FadeIn, isOpenClose, false)
        );
    }

    private void TweenOff(bool isOpenClose, CancellationTokenSource tokenToCancel)
    {
        IsPanelShown = false;
        OnPanelFadeFinish(FadeType.FadeOut, isOpenClose);
        Tween(
            this,
            false,
            m_OneLengthSelfArray,
            () =>
            {
                InvokeOnPanelFadeFinish(FadeType.FadeOut, isOpenClose, false);
                if (isOpenClose) tokenToCancel?.Cancel();
            }
        );
    }

    private void Tween(Node key, bool isOn, ReadOnlySpan<Control> controls, Action onFinish = null)
    {
        if (controls.Length == 0)
        {
            onFinish?.Invoke();
            return;
        }

        var duration = GetPanelTransitionDuration();
        var targetValue = isOn ? 1 : 0;


        if (!Mathf.IsZeroApprox(duration))
        {
            AdpUIPanelManager.DoTween(key, targetValue, duration, onFinish, controls);
        }
        else
        {
            foreach (var control in controls)
            {
                control.Modulate = new(control.Modulate, targetValue);
            }

            onFinish?.Invoke();
        }
    }
}
