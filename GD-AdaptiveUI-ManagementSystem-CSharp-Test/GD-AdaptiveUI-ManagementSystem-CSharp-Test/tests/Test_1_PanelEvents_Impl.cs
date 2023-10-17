using System;
using DEYU.GDUtilities.AdpUiManagementSystem.Panels;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class Test_1_PanelEvents_Impl : TestModule
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

    [Export] private PackedScene TestPanel { get; set; }

    public override async GDTask RunTestAsync()
    {
        TestHelpers.Log("Open next panel after 2");
        await GDTask.Delay(TimeSpan.FromSeconds(1));
        TestHelpers.Log("Open next panel after 1");
        await GDTask.Delay(TimeSpan.FromSeconds(1));
        
        var prepareBufferedPanel = TestHelpers.Run(() => TestPanel.PrepareBufferedPanel<UiPanel>());

        TestHelpers.Run(
            () =>
                prepareBufferedPanel.OpenPanelStack(
                    onPanelCloseCallback :
                    panel => TestHelpers.Log($"OpenPanelStack::onPanelCloseCallback({panel.Name})")
                )
        );
        await GDTask.WaitUntilCanceled(prepareBufferedPanel.OnPanelCloseToken);

        TestHelpers.Log("Open next panel after 2");
        await GDTask.Delay(TimeSpan.FromSeconds(1));
        TestHelpers.Log("Open next panel after 1");
        await GDTask.Delay(TimeSpan.FromSeconds(1));
        
        //prepareBufferedPanel = TestHelpers.Run(() => TestPanel.PrepareBufferedPanel<UiPanel>());
        
        TestHelpers.Run(
            () =>
                prepareBufferedPanel.OpenPanelStackA(
                    onPanelCloseCallback :
                    () => TestHelpers.Log("OpenPanelStackA::onPanelCloseCallback")
                )
        );
        await GDTask.WaitUntilCanceled(prepareBufferedPanel.OnPanelCloseToken);
    }
}
