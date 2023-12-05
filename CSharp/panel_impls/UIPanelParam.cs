using System;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanelParam<TOpenParam, TCloseParam> : UIPanelBaseImpl
{
    protected TOpenParam OpenParam { get; private set; }
    private TCloseParam CloseParam { get; set; }

    protected sealed override void OnPanelOpen() => OnPanelOpen(OpenParam);

    protected abstract void OnPanelOpen(TOpenParam openParam);

    protected sealed override void OnPanelClose() => OnPanelClose(CloseParam);

    protected virtual void OnPanelClose(TCloseParam closeParam) { }

    internal void OpenPanel
        (
            TOpenParam openParam,
            Action<UIPanelBaseImpl, TCloseParam> onPanelCloseCallback,
            PanelLayer currentPanelLayer,
            LayerVisual lastLayerVisual
        )
    {
        OpenParam = openParam;
        base.OpenPanel(
            x => onPanelCloseCallback?.Invoke(x, CloseParam),
            currentPanelLayer,
            lastLayerVisual
        );
    }

    protected void ClosePanel(TCloseParam closeParam)
    {
        CloseParam = closeParam;
        ClosePanelInternal();
    }

    protected void ClosePanelSilent(TCloseParam closeParam)
    {
        CloseParam = closeParam;
        ClosePanelSilentInternal();
    }
}