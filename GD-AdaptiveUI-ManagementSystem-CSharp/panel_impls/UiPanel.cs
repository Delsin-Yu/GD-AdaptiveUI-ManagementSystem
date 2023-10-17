using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanel : UiPanelBaseImpl
{
    protected void ClosePanel() => ClosePanelInternal();
    protected void ClosePanelSilent() => ClosePanelSilentInternal();

    protected void DisableCloseWithCancelKey() => RemovePanelWiseCancel(ClosePanel);
    protected void EnableCloseWithCancelKey() => RegisterPanelWiseCancel(ClosePanel);
}
