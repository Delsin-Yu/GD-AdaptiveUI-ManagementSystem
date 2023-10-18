using Godot;
using System;
using System.Collections.Generic;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    public static float PanelTransitionDuration { get => Impl.PanelTransitionDurationImpl; set => Impl.PanelTransitionDurationImpl = value; }

    public static Action<string> LogHandler { set => Impl.LogHandlerImpl = value; }
    public static Action<string> LogWarningHandler { set => Impl.LogWarningHandlerImpl = value; }
    public static Action<string> LogErrorHandler { set => Impl.LogErrorHandlerImpl = value; }
    
    internal static void Log(string message) => Impl.Log(message);
    internal static void LogWarning(string message) => Impl.LogWarning(message);
    internal static void LogError(string message) => Impl.LogError(message);
    
    private partial class AdpUIPanelManagerImpl
    {
        public Action<string> LogHandlerImpl { private get; set; } = GD.Print;
        public Action<string> LogWarningHandlerImpl { private get; set; } = GD.PushWarning;
        public Action<string> LogErrorHandlerImpl { private get; set; } = GD.PrintErr;

        public _AdpUIAudioInterfaceImpl AudioInterfaceImpl { get; }
        public _AdpUIInputInterceptorImpl InputInterceptorImpl { get; }

        public AdpUIPanelManagerImpl(_AdpUIAudioInterfaceImpl audioInterfaceImpl, _AdpUIInputInterceptorImpl inputInterceptorImpl)
        {
            AudioInterfaceImpl = audioInterfaceImpl;
            InputInterceptorImpl = inputInterceptorImpl;

            AudioInterfaceImpl.Load();
            InputInterceptorImpl.Load();
        }

        public void Log(string message) => LogHandlerImpl?.Invoke(message);
        public void LogWarning(string message) => LogWarningHandlerImpl?.Invoke(message);
        public void LogError(string message) => LogErrorHandlerImpl?.Invoke(message);
        
        private readonly Stack<Stack<UIPanelBaseImpl>> m_PanelStack = new();
        public float PanelTransitionDurationImpl { get; set; } = 0.1f;
    }
}
