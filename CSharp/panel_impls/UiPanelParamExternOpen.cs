namespace DEYU.GDUtilities.AdpUiManagementSystem.Panels;

public abstract partial class UiPanelParamExternOpen<TOpenParam> : UiPanelParamOpen<TOpenParam>
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
