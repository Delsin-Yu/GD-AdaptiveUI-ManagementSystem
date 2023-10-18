using System;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_0_AdpUILoader_Impl : TestModule
{
    [Export] private AudioStream TestAudio { get; set; }

    public override async GDTask RunTestAsync()
    {
        for (var i = 0; i < 5; i++)
        {
            TestHelpers.Run(() => AdpUIPanelManager.PlayAudio(TestAudio));
            await GDTask.DelayFrame(1);
        }

        TestHelpers.Run(() => AdpUIPanelManager.PlayAudio(null));
        TestHelpers.Run(AdpUIPanelManager.GetCancelActionName);
    }
}
