namespace DEYU.GDUtilities.AdpUIManagementSystem.Core;

public abstract partial class UIPanelBaseImpl
{
    private void OnPanelInitialize_Protected() => AdpUIPanelManager.RunProtected(OnPanelInitialize, "Initialization", "initializing panel", Name);
    private void OnPanelOpen_Protected() => AdpUIPanelManager.RunProtected(OnPanelOpen, "Open", "opening panel", Name);
    private void OnPanelClose_Protected() => AdpUIPanelManager.RunProtected(OnPanelClose, "Close", "closing panel", Name);
    private void OnPanelDestroyed_Protected() => AdpUIPanelManager.RunProtected(OnPanelDestroyed, "Destroy", "destroying panel", Name);
    internal uint RequestedInputScheme_Protected => RequestedInputScheme;
    private void OnPanelFadeStart_Protected(FadeType fadeType, bool isOpenClose) => OnPanelFadeStart(fadeType, isOpenClose);
    private void OnPanelFadeFinish_Protected(FadeType fadeType, bool isOpenClose) => OnPanelFadeFinish(fadeType, isOpenClose);
    private bool MutePanelOpenAudio_Protected => MutePanelOpenAudio;
    private bool MutePanelCloseAudio_Protected => MutePanelCloseAudio;
    private void GetPanelTransitionDuration_Protected() => GetPanelTransitionDuration();
}
