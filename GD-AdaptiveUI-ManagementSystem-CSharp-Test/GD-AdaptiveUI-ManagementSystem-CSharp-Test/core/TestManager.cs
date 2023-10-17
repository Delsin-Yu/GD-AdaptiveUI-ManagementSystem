using System;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class TestManagerImpl : Node
{
    [Export] private TestModule[] TestModules { get; set; }

    public override void _Ready() => RunTestImplAsync().Forget(exception => throw exception);

    private async GDTask RunTestImplAsync()
    {
        TestException testException = null;
        
        AdpUiPanelManager.LogHandler = TestHelpers.Log;
        AdpUiPanelManager.LogWarningHandler = TestHelpers.LogWarning;
        AdpUiPanelManager.LogErrorHandler = TestHelpers.LogError;
        
        foreach (var module in TestModules)
        {
            if(module.Exclude) continue;
            try
            {
                TestHelpers.Log($"Test Module: {module.Name}");
                await module.RunTestAsync();
            }
            catch (Exception e)
            {
                testException = new(module, e);
                break;
            }
        }

        if (testException != null) throw testException;

        TestHelpers.Log("Test Finish");
    }
}
