using System;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanelParamClose<TCloseParam> : UIPanelBaseImpl
{
    private TCloseParam CloseParam { get; set; }

    protected sealed override void OnPanelClose() => OnPanelClose(CloseParam);

    protected virtual void OnPanelClose(TCloseParam closeParam) { }

    internal void OpenPanel
        (
            Action<UIPanelBaseImpl, TCloseParam> onPanelCloseCallback,
            PanelLayer currentPanelLayer,
            LayerVisual lastLayerVisual
        ) =>
        base.OpenPanel(
            x => onPanelCloseCallback?.Invoke(x, CloseParam),
            currentPanelLayer,
            lastLayerVisual
        );

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