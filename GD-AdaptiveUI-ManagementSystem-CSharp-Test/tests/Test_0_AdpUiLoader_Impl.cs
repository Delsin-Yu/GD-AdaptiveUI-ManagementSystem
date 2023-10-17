using System;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class Test_0_AdpUiLoader_Impl : TestModule
{
    [Export] private AudioStream TestAudio { get; set; }

    public override async GDTask RunTestAsync()
    {
        for (var i = 0; i < 5; i++)
        {
            TestHelpers.Run(() => AdpUiPanelManager.PlayAudio(TestAudio));
            await GDTask.DelayFrame(5);
        }

        await GDTask.Delay(TimeSpan.FromSeconds(1));

        TestHelpers.Run(() => AdpUiPanelManager.PlayAudio(null));
        TestHelpers.Run(AdpUiPanelManager.GetCancelActionName);
    }
}
