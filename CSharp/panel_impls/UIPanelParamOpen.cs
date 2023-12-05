using System;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanelParamOpen<TOpenParam> : UIPanelBaseImpl
{
    protected TOpenParam OpenParam { get; private set; }

    protected sealed override void OnPanelOpen() => OnPanelOpen(OpenParam);

    protected abstract void OnPanelOpen(TOpenParam openParam);

    internal void OpenPanel
        (
            TOpenParam openParam,
            Action<UIPanelBaseImpl> onPanelCloseCallback,
            PanelLayer currentPanelLayer,
            LayerVisual lastLayerVisual
        )
    {
        OpenParam = openParam;
        base.OpenPanel(
            onPanelCloseCallback,
            currentPanelLayer,
            lastLayerVisual
        );
    }

    protected void ClosePanel() => ClosePanelInternal();

    protected void ClosePanelSilent() => ClosePanelSilentInternal();

    protected void DisableCloseWithCancelKey() => RemovePanelWiseCancel(ClosePanelInternal);

    protected void EnableCloseWithCancelKey() => RegisterPanelWiseCancel(ClosePanelInternal);
}