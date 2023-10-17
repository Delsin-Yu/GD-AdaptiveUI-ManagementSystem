using System;
using System.Collections.Generic;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;
using DEYU.GDUtilities.AdpUiManagementSystem.Panels;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public static partial class AdpUiPanelManager
{
    public static void OpenPanelStack<TPanel>
        (
            this TPanel panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TPanel> onPanelCloseCallback = null
        ) where TPanel : UiPanel =>
        Impl.PushPanelToPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode)
            .OpenPanel(closed => onPanelCloseCallback?.Invoke((TPanel)closed), panelOpenMode, lastPanelVisualMode);

    public static void OpenPanelStack<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TPanel> onPanelCloseCallback = null
        ) where TPanel : UiPanelParamOpen<TOpenParam> =>
        Impl.PushPanelToPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode)
            .OpenPanel(openParam, closed => onPanelCloseCallback?.Invoke((TPanel)closed), panelOpenMode, lastPanelVisualMode);

    public static void OpenPanelStack<TCloseParam>
        (
            this UiPanelParamClose<TCloseParam> panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TCloseParam> onPanelCloseCallback = null
        ) =>
        Impl.PushPanelToPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode)
            .OpenPanel((_, param) => onPanelCloseCallback?.Invoke(param), panelOpenMode, lastPanelVisualMode);

    public static void OpenPanelStack<TOpenParam, TCloseParam>
        (
            this UiPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action<TCloseParam> onPanelCloseCallback = null
        ) =>
        Impl
           .PushPanelToPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode)
           .OpenPanel(openParam, (_, param) => onPanelCloseCallback?.Invoke(param), panelOpenMode, lastPanelVisualMode);

    public static void OpenPanelStackA<TPanel>
        (
            this TPanel panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action onPanelCloseCallback = null
        ) where TPanel : UiPanel =>
        OpenPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TPanel, TOpenParam>
        (
            this TPanel panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action onPanelCloseCallback = null
        ) where TPanel : UiPanelParamOpen<TOpenParam> =>
        OpenPanelStack(panelInstance, openParam, panelOpenMode, lastPanelVisualMode, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TCloseParam>
        (
            this UiPanelParamClose<TCloseParam> panelInstance,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action onPanelCloseCallback = null
        ) =>
        OpenPanelStack(panelInstance, panelOpenMode, lastPanelVisualMode, _ => onPanelCloseCallback?.Invoke());

    public static void OpenPanelStackA<TOpenParam, TCloseParam>
        (
            this UiPanelParam<TOpenParam, TCloseParam> panelInstance,
            TOpenParam openParam,
            PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
            PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual,
            Action onPanelCloseCallback = null
        ) =>
        OpenPanelStack(panelInstance, openParam, panelOpenMode, lastPanelVisualMode, _ => onPanelCloseCallback?.Invoke());

    private partial class AdpUiPanelManagerImpl
    {
        public T PushPanelToPanelStack<T>
            (
                T panelInstance,
                PanelOpenMode panelOpenMode = PanelOpenMode.DisableCurrentUi,
                PanelVisualMode lastPanelVisualMode = PanelVisualMode.PreserveVisual
            ) where T : UiPanelBaseImpl
        {
            Stack<UiPanelBaseImpl> focusingPanelStack;

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
            if (panelOpenMode == PanelOpenMode.PreserveCurrentUi)
            {
                if (m_PanelStack.Count > 0)
                {
                    if (m_PanelStack.Count == 0) PushPanelStack();

                    focusingPanelStack = m_PanelStack.Peek();
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
                        item.SetPanelActiveState(false, lastPanelVisualMode);
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
