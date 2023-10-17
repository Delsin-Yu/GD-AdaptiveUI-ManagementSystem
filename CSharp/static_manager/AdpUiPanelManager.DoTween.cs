using System;
using System.Collections.Generic;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public partial class AdpUiPanelManager
{
    internal static void DoTween(Node key, float targetAlpha, double duration, Action onFinish, ReadOnlySpan<Control> controls) =>
        Impl.DoTweenImpl(key, targetAlpha, duration, onFinish, controls);

    private partial class AdpUiPanelManagerImpl
    {
        private readonly Dictionary<Node, Tween> m_ActiveTweenInfo = new();
        private readonly NodePath m_CanvasItemModulateNodePath = new(CanvasItem.PropertyName.Modulate);

        public void DoTweenImpl(Node key, float targetAlpha, double duration, Action onFinish, ReadOnlySpan<Control> controls)
        {
            if (m_ActiveTweenInfo.Remove(key, out var lastTween))
            {
                lastTween.Kill();
            }

            var newTween = key.CreateTween();
            newTween.SetParallel();
            var targetColor = Variant.From(new Color(1, 1, 1, targetAlpha));
            foreach (var control in controls)
            {
                control.Modulate = new(1, 1, 1, 1 - targetAlpha);
                newTween.TweenProperty(
                    control,
                    m_CanvasItemModulateNodePath,
                    targetColor,
                    duration
                );
            }

            m_ActiveTweenInfo.Add(key, newTween);
            newTween.Finished +=
                () =>
                {
                    m_ActiveTweenInfo.Remove(key);
                    onFinish?.Invoke();
                };
        }
    }
}
