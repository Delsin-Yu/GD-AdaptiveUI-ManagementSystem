using System;
using System.Collections.Generic;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public static partial class AdpUiPanelManager
{
    public static T PrepareBufferedPanel<T>(this PackedScene panelPrefab, Action<T> preInitializeCallback = null) where T : UiPanelBaseImpl =>
        Impl.PrepareBufferedPanelImpl(panelPrefab, preInitializeCallback);

    public static T InstantiateTempPanel<T>(this PackedScene panelPrefab, Action<T> preInitializeCallback = null) where T : UiPanelBaseImpl =>
        Impl.InstantiateTempPanelImpl(panelPrefab, preInitializeCallback);

    public static bool TryDeleteBufferedPanel(PackedScene panelPrefab) => Impl.TryDeleteBufferedPanelImpl(panelPrefab);

    public static void PushActivePanelTransform(Node scriptOwner, Control panelContainer) => Impl.PushActivePanelTransformImpl(scriptOwner, panelContainer);
    
    public static void PopActivePanelTransform(Node scriptOwner) => Impl.PopActivePanelTransformImpl(scriptOwner);
    
    private partial class AdpUiPanelManagerImpl
    {
        private Dictionary<PackedScene, UiPanelBaseImpl> BufferedPanel { get; } = new();
        private readonly Stack<(Node ScriptOwner, Control Root)> m_ActivePanelTransform = new();
      
        public T PrepareBufferedPanelImpl<T>(PackedScene panelPrefab, Action<T> preInitializeCallback = null) where T : UiPanelBaseImpl
        {
            T panelInstance;
            if (BufferedPanel.TryGetValue(panelPrefab, out var panelInstanceSrc))
            {
                panelInstance = (T)panelInstanceSrc;
                preInitializeCallback?.Invoke(panelInstance);
            }
            else panelInstance = GenerateBufferedPanel(panelPrefab, preInitializeCallback);

            return panelInstance;
        }

        public T InstantiateTempPanelImpl<T>(PackedScene panelPrefab, Action<T> preInitializeCallback = null) where T : UiPanelBaseImpl =>
            InstantiatePanel(panelPrefab, true, preInitializeCallback);

        private T GenerateBufferedPanel<T>(PackedScene panelPrefab, Action<T> preInitializeCallback) where T : UiPanelBaseImpl
        {
            var panelInstance = InstantiatePanel(panelPrefab, false, preInitializeCallback);
            var currentPanelParent = panelPrefab;

            panelInstance.OnPanelOpenCloseFadeFinishCallbackInternal +=
                states =>
                {
                    if (states == FadeType.FadeOut) BufferPanel(currentPanelParent);
                };

            BufferedPanel.Add(panelPrefab, panelInstance);
            return panelInstance;
        }

        private T InstantiatePanel<T>(PackedScene panelParent, bool destroyPanelAfterClose, Action<T> preInitializeCallback = null) where T : UiPanelBaseImpl
        {
            var panelNode = panelParent.Instantiate();

            var panelInstance = (T)panelNode;

            GetCurrentPanelRoot().AddChild(panelInstance);

            if (panelInstance == null) throw new ArgumentException($"{panelParent.ResourcePath} 不含有类型为 {typeof(T).Name} 的组件！");

            preInitializeCallback?.Invoke(panelInstance);

            if (destroyPanelAfterClose)
                panelInstance.OnPanelOpenCloseFadeFinishCallbackInternal +=
                    fadeType =>
                    {
                        if (fadeType != FadeType.FadeOut) return;

                        if (!GodotObject.IsInstanceValid(panelInstance)) LogWarning($"{nameof(panelInstance)} 在被释放前已经被销毁！");

                        panelInstance.Free();
                    };


            panelInstance.Initialize(destroyPanelAfterClose);
            return panelInstance;
        }

        private void BufferPanel(PackedScene panelPrefab)
        {
            if (BufferedPanel.TryGetValue(panelPrefab, out var panelInstance)) TogglePanel(panelInstance, false);
            else throw new KeyNotFoundException($"The Parent: {panelPrefab.ResourcePath} key is not found inside the Panel collection buffer");
        }

        public void PushActivePanelTransformImpl(Node scriptOwner, Control panelContainer) =>
            //Debug.LogError("Push ControlledPanelTransform: " + transform.name, transform);
            m_ActivePanelTransform.Push((scriptOwner, panelContainer));

        public void PopActivePanelTransformImpl(Node scriptOwner)
        {
            if (m_ActivePanelTransform.Peek().ScriptOwner != scriptOwner)
            {
                var root = m_ActivePanelTransform.Peek().Root;
                LogError($"无法弹出目标Rect({root.Name})!，产生请求的所有者: ({scriptOwner.Name})和原所有者: ({m_ActivePanelTransform.Peek().ScriptOwner.Name})不相符！");
                throw new InvalidOperationException();
            }
            
            m_ActivePanelTransform.Pop();
            //Debug.LogError("Pop ControlledPanelTransform: " + rectTransform.Root.name, rectTransform.Root);
        }

        private Control GetCurrentPanelRoot() => m_ActivePanelTransform.Peek().Root;

        public bool TryDeleteBufferedPanelImpl(PackedScene panelPrefab)
        {
            if (!BufferedPanel.Remove(panelPrefab, out var uiPanelBaseImpl)) return false;

            uiPanelBaseImpl.ReleasingBufferedPanel = true;
            uiPanelBaseImpl.QueueFree();
            return true;
        }
    }
}
