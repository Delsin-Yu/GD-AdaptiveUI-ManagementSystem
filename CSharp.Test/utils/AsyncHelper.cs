using System;
using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Fractural.Tasks;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public static class AsyncHelper
{
    public static GDTask OpenPanelStackAsync<TPanel>
        (
            this TPanel panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Visible,
            Action<TPanel> onPanelCloseCallbackImmediate = null
        ) where TPanel : UIPanel
    {
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(panelLayer, lastLayerVisual, onPanelCloseCallbackImmediate);
        return GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
    }

    public static GDTask OpenPanelStackAsync<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Visible,
            Action<TPanel> onPanelCloseCallbackImmediate = null
        ) where TPanel : UIPanelParamOpen<TOpenParam>
    {
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(openParam, panelLayer, lastLayerVisual, onPanelCloseCallbackImmediate);
        return GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
    }

    public static async GDTask<TCloseParam> OpenPanelStackAsync<TCloseParam>
        (
            this UIPanelParamClose<TCloseParam> panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Visible,
            Action<TCloseParam> onPanelCloseCallbackImmediate = null
        )
    {
        var panelReturnValue = default(TCloseParam);
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(panelLayer, lastLayerVisual,
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
            this UIPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Visible,
            Action<TCloseParam> onPanelCloseCallbackImmediate = null
        )
    {
        var panelReturnValue = default(TCloseParam);
        var token = panelInstance.OnPanelCloseToken;
        panelInstance.OpenPanelStack(openParam, panelLayer, lastLayerVisual,
            returnValue =>
            {
                panelReturnValue = returnValue;
                onPanelCloseCallbackImmediate?.Invoke(returnValue);
            });
        await GDTask.WaitUntilCanceled(token).SuppressCancellationThrow();
        return panelReturnValue;   
    }
}
