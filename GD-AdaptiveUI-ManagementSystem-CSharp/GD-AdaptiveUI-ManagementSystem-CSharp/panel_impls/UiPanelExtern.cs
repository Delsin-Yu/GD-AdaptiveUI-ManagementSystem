namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanelExtern : UiPanel
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
