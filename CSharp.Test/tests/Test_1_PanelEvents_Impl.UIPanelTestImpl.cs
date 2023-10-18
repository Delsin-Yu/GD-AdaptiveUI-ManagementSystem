using DEYU.GDUtilities.AdpUIManagementSystem.Panels;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_1_PanelEvents_Impl
{
    public partial class UIPanelTestImpl : UIPanel
    {
        protected override void OnPanelOpen() => TestHelpers.Log("UIPanel::OnPanelOpen");

        protected override void OnPanelInitialize()
        {
            base.OnPanelInitialize();
            TestHelpers.Log("UIPanel::OnPanelInitialize");
            EnableCloseWithCancelKey();
        }

        protected override void OnPanelClose()
        {
            base.OnPanelClose();
            TestHelpers.Log("UIPanel::OnPanelClose");
        }

        protected override void OnPanelDestroyed()
        {
            base.OnPanelDestroyed();
            TestHelpers.Log("UIPanel::OnPanelDestroyed");
        }

        protected override void OnPanelFadeStart(FadeType fadeType, bool isOpenClose)
        {
            base.OnPanelFadeStart(fadeType, isOpenClose);
            TestHelpers.Log($"UIPanel::OnPanelFadeStart(fadeType:{fadeType}, isOpenClose: {isOpenClose})");
        }

        protected override void OnPanelFadeFinish(FadeType fadeType, bool isOpenClose)
        {
            base.OnPanelFadeFinish(fadeType, isOpenClose);
            TestHelpers.Log($"UIPanel::OnPanelFadeFinish(fadeType:{fadeType}, isOpenClose: {isOpenClose})");
        }
    }
}
