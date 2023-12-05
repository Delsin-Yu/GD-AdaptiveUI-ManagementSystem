using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanel : UIPanelBaseImpl
{
    protected void ClosePanel() => ClosePanelInternal();
    protected void ClosePanelSilent() => ClosePanelSilentInternal();

    protected void DisableCloseWithCancelKey() => RemovePanelWiseCancel(ClosePanel);
    protected void EnableCloseWithCancelKey() => RegisterPanelWiseCancel(ClosePanel);
}