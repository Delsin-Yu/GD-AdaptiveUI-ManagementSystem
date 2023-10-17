using System;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanelParamOpen<TOpenParam> : UiPanelBaseImpl
{
    protected TOpenParam OpenParam { get; private set; }

    protected sealed override void OnPanelOpen() => OnPanelOpen(OpenParam);

    protected abstract void OnPanelOpen(TOpenParam openParam);

    internal void OpenPanel
        (
            TOpenParam openParam,
            Action<UiPanelBaseImpl> onPanelCloseCallback,
            PanelOpenMode currentPanelOpenMode,
            PanelVisualMode lastPanelVisualMode
        )
    {
        OpenParam = openParam;
        base.OpenPanel(
            onPanelCloseCallback,
            currentPanelOpenMode,
            lastPanelVisualMode
        );
    }

    protected void ClosePanel() => ClosePanelInternal();

    protected void ClosePanelSilent() => ClosePanelSilentInternal();

    protected void DisableCloseWithCancelKey() => RemovePanelWiseCancel(ClosePanelInternal);

    protected void EnableCloseWithCancelKey() => RegisterPanelWiseCancel(ClosePanelInternal);
}
