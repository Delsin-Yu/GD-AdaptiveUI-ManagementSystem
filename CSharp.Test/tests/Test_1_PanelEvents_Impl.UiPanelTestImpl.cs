using DEYU.GDUtilities.AdpUiManagementSystem.Panels;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class Test_1_PanelEvents_Impl
{
    public partial class UiPanelTestImpl : UiPanel
    {
        protected override void OnPanelOpen() => TestHelpers.Log("UiPanel::OnPanelOpen");

        protected override void OnPanelInitialize()
        {
            base.OnPanelInitialize();
            TestHelpers.Log("UiPanel::OnPanelInitialize");
            EnableCloseWithCancelKey();
        }

        protected override void OnPanelClose()
        {
            base.OnPanelClose();
            TestHelpers.Log("UiPanel::OnPanelClose");
        }

        protected override void OnPanelDestroyed()
        {
            base.OnPanelDestroyed();
            TestHelpers.Log("UiPanel::OnPanelDestroyed");
        }

        protected override void OnPanelFadeStart(FadeType fadeType, bool isOpenClose)
        {
            base.OnPanelFadeStart(fadeType, isOpenClose);
            TestHelpers.Log($"UiPanel::OnPanelFadeStart(fadeType:{fadeType}, isOpenClose: {isOpenClose})");
        }

        protected override void OnPanelFadeFinish(FadeType fadeType, bool isOpenClose)
        {
            base.OnPanelFadeFinish(fadeType, isOpenClose);
            TestHelpers.Log($"UiPanel::OnPanelFadeFinish(fadeType:{fadeType}, isOpenClose: {isOpenClose})");
        }
    }
}
