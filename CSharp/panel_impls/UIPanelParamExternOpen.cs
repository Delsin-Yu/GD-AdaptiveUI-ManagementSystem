namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanelParamExternOpen<TOpenParam> : UIPanelParamOpen<TOpenParam>
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