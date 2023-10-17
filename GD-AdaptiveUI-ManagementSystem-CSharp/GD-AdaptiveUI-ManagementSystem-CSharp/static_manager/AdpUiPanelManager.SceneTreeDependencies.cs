using System;
using System.Threading;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

public static partial class AdpUiPanelManager
{
    private const string DefaultAssetPath = 
        "res://addons/deyu_adaptive_ui_management_system/adaptive_ui_management_system.tscn";
    
    private static AdpUiPanelManagerImpl s_ImplInstance;

    private static AdpUiPanelManagerImpl Impl
    {
        get
        {
            if (s_ImplInstance != null) return s_ImplInstance;
            
            var loader = (_AdpUiLoaderImpl)GD.Load<PackedScene>(DefaultAssetPath).Instantiate();
            if (Engine.GetMainLoop() is not SceneTree sceneTree)
            {
                throw new NotSupportedException("Custom main loop is not supported.");
            }

            var currentScene = sceneTree.CurrentScene;
            currentScene.AddChild(loader);
            currentScene.MoveChild(loader, -1);
            SetupSceneTreeDependencies(loader.AudioInterfaceImpl, loader.InputInterceptorImpl);
            PushActivePanelTransform(loader, loader.DefaultPanelRoot);
            
            return s_ImplInstance;
        }
    }

    internal static void SetupSceneTreeDependencies(_AdpUiAudioInterfaceImpl audioInterfaceImpl, _AdpUiInputInterceptorImpl inputInterceptorImpl) => 
        s_ImplInstance = new(audioInterfaceImpl, inputInterceptorImpl);

#region Audio Interface
    public static void PlayAudio(AudioStream audioStream) => 
        Impl.AudioInterfaceImpl.PlayAudio(audioStream);

    internal static void PlayDefaultPanelCloseAudio() => 
        Impl.AudioInterfaceImpl.PlayDefaultPanelCloseAudio();

    internal static void PlayDefaultPanelOpenAudio() => 
        Impl.AudioInterfaceImpl.PlayDefaultPanelOpenAudio();

#endregion

#region Input Interface
    public static string GetCancelActionName() =>
        Impl.InputInterceptorImpl.CancelActionName;

    private static void UpdateInputScheme(uint requestedInputScheme) => 
        Impl.InputInterceptorImpl.UpdateInputScheme(requestedInputScheme);

#endregion
}
