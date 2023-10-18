using System;
using DEYU.GDUtilities.AdpUiManagementSystem.Panels;
using Fractural.Tasks;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public static class AsyncHelper
{
    public static GDTask OpenPanelStackAsync<TPanel>
        (
            this TPanel panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TPanel> onPanelCloseCallbackImmediate = null
        ) where TPanel : UiPanel
    {
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(panelOpenMode, lastPanelVisualMode, onPanelCloseCallbackImmediate);
        return GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
    }

    public static GDTask OpenPanelStackAsync<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TPanel> onPanelCloseCallbackImmediate = null
        ) where TPanel : UiPanelParamOpen<TOpenParam>
    {
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(openParam, panelOpenMode, lastPanelVisualMode, onPanelCloseCallbackImmediate);
        return GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
    }

    public static async GDTask<TCloseParam> OpenPanelStackAsync<TCloseParam>
        (
            this UiPanelParamClose<TCloseParam> panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TCloseParam> onPanelCloseCallbackImmediate = null
        )
    {
        var panelReturnValue = default(TCloseParam);
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(panelOpenMode, lastPanelVisualMode,
            returnValue =>
            {
                panelReturnValue = returnValue;
                onPanelCloseCallbackImmediate?.Invoke(returnValue);
            });
        await GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
        return panelReturnValue;
    }

    public static async GDTask<TCloseParam> OpenPanelStackAsync<TOpenParam, TCloseParam>
        (
            this UiPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TCloseParam> onPanelCloseCallbackImmediate = null
        )
    {
        var panelReturnValue = default(TCloseParam);
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(openParam, panelOpenMode, lastPanelVisualMode,
            returnValue =>
            {
                panelReturnValue = returnValue;
                onPanelCloseCallbackImmediate?.Invoke(returnValue);
            });
        await GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
        return panelReturnValue;   
    }
}
