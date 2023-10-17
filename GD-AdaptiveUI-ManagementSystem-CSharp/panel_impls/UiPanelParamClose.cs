using System;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanelParamClose<TCloseParam> : UiPanelBaseImpl
{
    private TCloseParam CloseParam { get; set; }

    protected sealed override void OnPanelClose() => OnPanelClose(CloseParam);

    protected virtual void OnPanelClose(TCloseParam closeParam) { }

    internal void OpenPanel
        (
            Action<UiPanelBaseImpl, TCloseParam> onPanelCloseCallback,
            PanelOpenMode currentPanelOpenMode,
            PanelVisualMode lastPanelVisualMode
        ) =>
        base.OpenPanel(
            x => onPanelCloseCallback?.Invoke(x, CloseParam),
            currentPanelOpenMode,
            lastPanelVisualMode
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
