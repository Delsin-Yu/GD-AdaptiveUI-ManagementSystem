using System;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Core;

public partial class UIPanelBaseImpl
{
    internal bool IsTempPanel { get; private set; }

    internal bool ReleasingBufferedPanel
    {
        set
        {
            if (value) OnPanelOpenCloseFadeFinishCallbackInternal = null;
            m_ReleasingBufferedPanel = value;
        }
        private get => m_ReleasingBufferedPanel;
    }

    internal event Action<FadeType> OnPanelOpenCloseFadeFinishCallbackInternal;
    internal void ClosePanelSilentInternal() => ClosePanelImpl();
    internal void ClosePanelSimulated()
    {
        ClosePanelImplShared();
        InvokeOnPanelFadeFinish(FadeType.FadeOut, true, true);
        OnPanelClose();
    }

    internal void Initialize(bool isTempPanel)
    {
        IsTempPanel = isTempPanel;
        m_Initialized = true;
        m_ActiveOnlyVisualElementsTweenKey = new();
        m_ActiveOnlyVisualElementsTweenKey.Name = "VisualElementsTweenKey";
        m_OneLengthSelfArray[0] = this;
        AddChild(m_ActiveOnlyVisualElementsTweenKey);
        OnPanelInitialize();
        PanelCloseFadeFinishTokenSource = new();
        PanelCloseTokenSource = new();
    }

    internal void OpenPanel
        (
            Action<UIPanelBaseImpl> onPanelCloseCallback,
            PanelLayer currentPanelLayer,
            LayerVisual lastLayerVisual
        )
    {
        if (!m_Initialized) throw new InvalidOperationException("禁止在未初始化面板的情况下开启面板");

        //Debug.LogError("Panel Opened: " + name, this);
        m_OnPanelCloseCallback = onPanelCloseCallback;
        TweenOn(true);
        IsPanelOpened = true;

        m_CurrentPanelLayer = currentPanelLayer;
        m_LastLayerVisual = lastLayerVisual;

        if (!MutePanelOpenAudio)
        {
            if (OverrideOnPanelOpenAudio != null) AdpUIPanelManager.PlayAudio(OverrideOnPanelOpenAudio);
            else AdpUIPanelManager.PlayDefaultPanelOpenAudio();
        }

        OnPanelOpen();
    }

    internal void SetPanelActiveState(bool active, LayerVisual layerVisual)
    {
        if (!active)
        {
            Control control = null;
            CacheCurrentSelection(ref control);

            if (layerVisual == LayerVisual.Hidden)
            {
                m_IsHiddenAtStart = !IsPanelShown;
                HidePanel();
            }

            AdpUIPanelManager.SetNodeChildAvailability(this, false);
        }
        else
        {
            AdpUIPanelManager.SetNodeChildAvailability(this, true);
            if (layerVisual == LayerVisual.Hidden)
                if (!m_IsHiddenAtStart)
                    ShowPanel();
        }

        Tween(m_ActiveOnlyVisualElementsTweenKey, active, ActiveOnlyVisualElements);
    }

    internal SelectionCacheResult CacheCurrentSelection(ref Control currentSelection)
    {
        BufferedSelection = null;
        currentSelection ??= GetViewport().GuiGetFocusOwner();
        if (currentSelection == null) return SelectionCacheResult.NoSelections;
        if (!IsAncestorOf(currentSelection)) return SelectionCacheResult.SelectionIsNotChild;
        BufferedSelection = currentSelection;
        return SelectionCacheResult.Successful;
    }

    internal void HandlePanelReselection(ref bool hasSuccessfullyRestoredSelection)
    {
        if (hasSuccessfullyRestoredSelection) return;

        if (BufferedSelection is null) return;

        hasSuccessfullyRestoredSelection = true;
        BufferedSelection.CallDeferred(Control.MethodName.GrabFocus);
        BufferedSelection = null;
    }
}
