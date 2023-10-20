using System;
using System.Collections.Generic;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;
using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    public static void OpenPanelStack<TPanel>
        (
            this TPanel panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action<TPanel> onPanelCloseCallback = null
        ) where TPanel : UIPanel =>
        Impl.PushPanelToPanelStack(panelInstance, panelLayer, lastLayerVisual)
            .OpenPanel(closed => onPanelCloseCallback?.Invoke((TPanel)closed), panelLayer, lastLayerVisual);

    public static void OpenPanelStack<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action<TPanel> onPanelCloseCallback = null
        ) where TPanel : UIPanelParamOpen<TOpenParam> =>
        Impl.PushPanelToPanelStack(panelInstance, panelLayer, lastLayerVisual)
            .OpenPanel(openParam, closed => onPanelCloseCallback?.Invoke((TPanel)closed), panelLayer, lastLayerVisual);

    public static void OpenPanelStack<TCloseParam>
        (
            this UIPanelParamClose<TCloseParam> panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action<TCloseParam> onPanelCloseCallback = null
        ) =>
        Impl.PushPanelToPanelStack(panelInstance, panelLayer, lastLayerVisual)
            .OpenPanel((_, param) => onPanelCloseCallback?.Invoke(param), panelLayer, lastLayerVisual);

    public static void OpenPanelStack<TOpenParam, TCloseParam>
        (
            this UIPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action<TCloseParam> onPanelCloseCallback = null
        ) =>
        Impl
           .PushPanelToPanelStack(panelInstance, panelLayer, lastLayerVisual)
           .OpenPanel(openParam, (_, param) => onPanelCloseCallback?.Invoke(param), panelLayer, lastLayerVisual);

    public static void OpenPanelStackA<TPanel>
        (
            this TPanel panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action onPanelCloseCallback = null
        ) where TPanel : UIPanel =>
        OpenPanelStack(panelInstance, panelLayer, lastLayerVisual, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action onPanelCloseCallback = null
        ) where TPanel : UIPanelParamOpen<TOpenParam> =>
        OpenPanelStack(panelInstance, openParam, panelLayer, lastLayerVisual, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TCloseParam>
        (
            this UIPanelParamClose<TCloseParam> panelInstance,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action onPanelCloseCallback = null
        ) =>
        OpenPanelStack(panelInstance, panelLayer, lastLayerVisual, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TOpenParam, TCloseParam>
        (
            this UIPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelLayer panelLayer = PanelLayer.NewLayer,
            LayerVisual lastLayerVisual = LayerVisual.Preserve,
            Action onPanelCloseCallback = null
        ) =>
        OpenPanelStack(panelInstance, openParam, panelLayer, lastLayerVisual, _ => onPanelCloseCallback?.Invoke());

    private partial class AdpUIPanelManagerImpl
    {
        public T PushPanelToPanelStack<T>
            (
                T panelInstance,
                PanelLayer panelLayer,
                LayerVisual lastLayerVisual
            ) where T : UIPanelBaseImpl
        {
            Stack<UIPanelBaseImpl> focusingPanelStack;

            // 确保当前界面处于当前面板根的最前端
            var parent = GetCurrentPanelRoot();
            var oldParent = panelInstance.GetParent();
            if (oldParent == parent) parent.MoveToFront();
            else
            {
                oldParent?.RemoveChild(panelInstance);

                parent.AddChild(panelInstance);
            }

            TogglePanel(panelInstance, true);

            // 在同层面板开启的情况下，生长当前面板栈
            if (panelLayer == PanelLayer.SameLayer)
            {
                if (m_PanelStack.Count > 0)
                {
                    if (m_PanelStack.Count == 0) PushPanelStack();

                    focusingPanelStack = m_PanelStack.Peek();
                    foreach (var item in focusingPanelStack)
                    {
                        if(item.CacheCurrentSelection() is SelectionCacheResult.Successful or SelectionCacheResult.NoSelections) break;
                    }
                }
                else
                {
                    throw new InvalidOperationException("在未开启任何面板的情况下，禁止以同层逻辑开启面板");
                }
            }
            // 在非同层面板开启的情况下，关闭上一个面板栈，并且建立新的面板栈
            else
            {
                // 如果有面版栈就设置为非活动
                if (m_PanelStack.Count > 0)
                {
                    var topmostPanelStack = m_PanelStack.Peek();
                    foreach (var item in topmostPanelStack)
                    {
                        item.SetPanelActiveState(false, lastLayerVisual);
                    }
                }

                // 非同层面板会建立新的面板栈
                PushPanelStack();
                focusingPanelStack = m_PanelStack.Peek();
            }

            // 将输入切换为面板所需的输入模式
            UpdateInputScheme(panelInstance.RequestedInputScheme);

            // 将当前面板加入到选出的栈
            focusingPanelStack.Push(panelInstance);

            Log($"[ADP UI] Open Panel: {panelInstance.Name}");
            return panelInstance;
        }

        private void PushPanelStack() => m_PanelStack.Push(new());
        private void PopPanelStack() => m_PanelStack.Pop();
    }
}
