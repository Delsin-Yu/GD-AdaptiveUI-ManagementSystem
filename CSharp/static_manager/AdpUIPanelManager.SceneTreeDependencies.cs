using System;
using System.Threading;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    private const string DefaultAssetPath = 
        "res://addons/deyu_adaptive_ui_management_system/adaptive_ui_management_system.tscn";
    
    private static AdpUIPanelManagerImpl s_ImplInstance;
    private static _AdpUILoaderImpl s_LoaderImpl;

    private static AdpUIPanelManagerImpl Impl
    {
        get
        {
            if (s_ImplInstance != null) return s_ImplInstance;

            if (s_LoaderImpl == null)
            {
                s_LoaderImpl =  (_AdpUILoaderImpl)GD.Load<PackedScene>(DefaultAssetPath).Instantiate();
                
                if (Engine.GetMainLoop() is not SceneTree sceneTree)
                {
                    throw new NotSupportedException("Custom main loop is not supported.");
                }
                
                var currentScene = sceneTree.CurrentScene;
                currentScene.AddChild(s_LoaderImpl);
                currentScene.MoveChild(s_LoaderImpl, -1);
            }
            
            SetupSceneTreeDependencies(s_LoaderImpl.AudioInterfaceImpl, s_LoaderImpl.InputInterceptorImpl);
            PushActivePanelTransform(s_LoaderImpl, s_LoaderImpl.DefaultPanelRoot);
            
            return s_ImplInstance;
        }
    }

    internal static void RegisterLoader(_AdpUILoaderImpl loaderImpl) => s_LoaderImpl = loaderImpl;
    
    private static void SetupSceneTreeDependencies(_AdpUIAudioInterfaceImpl audioInterfaceImpl, _AdpUIInputInterceptorImpl inputInterceptorImpl) => 
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
