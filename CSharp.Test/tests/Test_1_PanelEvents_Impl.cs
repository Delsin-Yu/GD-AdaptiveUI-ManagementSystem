using System;
using System.Threading.Tasks;
using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_1_PanelEvents_Impl : TestModule
{
    [Export] private PackedScene TestPanel { get; set; }

    public override async GDTask RunTestAsync()
    {

        await TestPrepare();
        await TestTemp();
    }
    
    private async Task TestTemp()
    {
        var tempPanel = TestHelpers.Run(() => TestPanel.InstantiateTempPanel<UIPanel>());

        TestHelpers.Run(
            () =>
                tempPanel.OpenPanelStack(
                    onPanelCloseCallback :
                    panel => TestHelpers.Log($"OpenPanelStack::onPanelCloseCallback({panel.Name})")
                )
        );
        await GDTask.WaitUntilCanceled(tempPanel.OnPanelCloseToken);

        tempPanel = TestHelpers.Run(() => TestPanel.InstantiateTempPanel<UIPanel>());

        TestHelpers.Run(
            () =>
                tempPanel.OpenPanelStackA(
                    onPanelCloseCallback :
                    () => TestHelpers.Log("OpenPanelStackA::onPanelCloseCallback")
                )
        );
        await GDTask.WaitUntilCanceled(tempPanel.OnPanelCloseToken);
    }

    private async Task TestPrepare()
    {
        var prepareBufferedPanel = TestHelpers.Run(() => TestPanel.PrepareBufferedPanel<UIPanel>());

        TestHelpers.Run(
            () =>
                prepareBufferedPanel.OpenPanelStack(
                    onPanelCloseCallback :
                    panel => TestHelpers.Log($"OpenPanelStack::onPanelCloseCallback({panel.Name})")
                )
        );
        await GDTask.WaitUntilCanceled(prepareBufferedPanel.OnPanelCloseToken);
        
        TestHelpers.Run(
            () =>
                prepareBufferedPanel.OpenPanelStackA(
                    onPanelCloseCallback :
                    () => TestHelpers.Log("OpenPanelStackA::onPanelCloseCallback")
                )
        );
        await GDTask.WaitUntilCanceled(prepareBufferedPanel.OnPanelCloseToken);

        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestPanel));
    }
}
