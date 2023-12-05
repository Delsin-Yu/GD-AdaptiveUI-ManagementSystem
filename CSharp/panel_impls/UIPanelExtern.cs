namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanelExtern : UIPanel
{
    protected virtual void OnExitExtern() { }

    public void CloseExternPanel()
    {
        OnExitExtern();
        ClosePanel();
    }

    public void CloseExternPanelSilent()
    {
        OnExitExtern();
        ClosePanelSilent();
    }
}