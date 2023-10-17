using System;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanelParam<TOpenParam, TCloseParam> : UiPanelBaseImpl
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
            Action<UiPanelBaseImpl, TCloseParam> onPanelCloseCallback,
            PanelOpenMode currentPanelOpenMode,
            PanelVisualMode lastPanelVisualMode
        )
    {
        OpenParam = openParam;
        base.OpenPanel(
            x => onPanelCloseCallback?.Invoke(x, CloseParam),
            currentPanelOpenMode,
            lastPanelVisualMode
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
