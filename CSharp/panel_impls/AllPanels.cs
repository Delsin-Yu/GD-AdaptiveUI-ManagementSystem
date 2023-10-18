using System;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Panels;

public abstract partial class UIPanel : UIPanelBaseImpl
{
    protected void ClosePanel() => ClosePanelInternal();
    protected void ClosePanelSilent() => ClosePanelSilentInternal();

    protected void DisableCloseWithCancelKey() => RemovePanelWiseCancel(ClosePanel);
    protected void EnableCloseWithCancelKey() => RegisterPanelWiseCancel(ClosePanel);
}

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

public abstract partial class UIPanelParam<TOpenParam, TCloseParam> : UIPanelBaseImpl
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
            Action<UIPanelBaseImpl, TCloseParam> onPanelCloseCallback,
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

public abstract partial class UIPanelParamOpen<TOpenParam> : UIPanelBaseImpl
{
    protected TOpenParam OpenParam { get; private set; }

    protected sealed override void OnPanelOpen() => OnPanelOpen(OpenParam);

    protected abstract void OnPanelOpen(TOpenParam openParam);

    internal void OpenPanel
        (
            TOpenParam openParam,
            Action<UIPanelBaseImpl> onPanelCloseCallback,
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

public abstract partial class UIPanelParamClose<TCloseParam> : UIPanelBaseImpl
{
    private TCloseParam CloseParam { get; set; }

    protected sealed override void OnPanelClose() => OnPanelClose(CloseParam);

    protected virtual void OnPanelClose(TCloseParam closeParam) { }

    internal void OpenPanel
        (
            Action<UIPanelBaseImpl, TCloseParam> onPanelCloseCallback,
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
