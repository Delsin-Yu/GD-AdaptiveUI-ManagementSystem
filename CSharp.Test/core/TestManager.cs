using System;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class TestManagerImpl : Control
{
    [Export] private TestModule[] TestModules { get; set; }

    public override void _Ready() => RunTestImplAsync().Forget(exception => throw exception);

    private async GDTask RunTestImplAsync()
    {
        AdpUIPanelManager.LogHandler = TestHelpers.Log;
        AdpUIPanelManager.LogWarningHandler = TestHelpers.LogWarning;
        AdpUIPanelManager.LogErrorHandler = TestHelpers.LogError;

        foreach (var module in TestModules)
        {
            if (module.Exclude) continue;
            TestHelpers.Log($"Test Module: {module.Name}");
            await module.RunTestAsync();
        }

        TestHelpers.Log("Test Finish");
        GetTree().Quit();
    }
}
