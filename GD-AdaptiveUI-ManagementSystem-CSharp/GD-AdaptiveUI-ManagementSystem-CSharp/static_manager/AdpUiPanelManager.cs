using Godot;
using System;
using System.Collections.Generic;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public static partial class AdpUiPanelManager
{
    public static float PanelTransitionDuration { get => Impl.PanelTransitionDurationImpl; set => Impl.PanelTransitionDurationImpl = value; }

    public static Action<string> LogHandler { set => Impl.LogHandlerImpl = value; }
    public static Action<string> LogWarningHandler { set => Impl.LogWarningHandlerImpl = value; }
    public static Action<string> LogErrorHandler { set => Impl.LogErrorHandlerImpl = value; }
    
    internal static void Log(string message) => Impl.Log(message);
    internal static void LogWarning(string message) => Impl.LogWarning(message);
    internal static void LogError(string message) => Impl.LogError(message);
    
    private partial class AdpUiPanelManagerImpl
    {
        public Action<string> LogHandlerImpl { private get; set; } = GD.Print;
        public Action<string> LogWarningHandlerImpl { private get; set; } = GD.PushWarning;
        public Action<string> LogErrorHandlerImpl { private get; set; } = GD.PrintErr;

        public _AdpUiAudioInterfaceImpl AudioInterfaceImpl { get; }
        public _AdpUiInputInterceptorImpl InputInterceptorImpl { get; }

        public AdpUiPanelManagerImpl(_AdpUiAudioInterfaceImpl audioInterfaceImpl, _AdpUiInputInterceptorImpl inputInterceptorImpl)
        {
            AudioInterfaceImpl = audioInterfaceImpl;
            InputInterceptorImpl = inputInterceptorImpl;

            AudioInterfaceImpl.Load();
            InputInterceptorImpl.Load();
        }

        public void Log(string message) => LogHandlerImpl?.Invoke(message);
        public void LogWarning(string message) => LogWarningHandlerImpl?.Invoke(message);
        public void LogError(string message) => LogErrorHandlerImpl?.Invoke(message);
        
        private readonly Stack<Stack<UiPanelBaseImpl>> m_PanelStack = new();
        public float PanelTransitionDurationImpl { get; set; } = 0.1f;
    }
}
