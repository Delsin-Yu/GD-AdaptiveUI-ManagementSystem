﻿using System;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class TestManagerImpl : Node
{
    [Export] private TestModule[] TestModules { get; set; }

    public override void _Ready() => RunTestImplAsync().Forget(exception => throw exception);

    private async GDTask RunTestImplAsync()
    {
        AdpUiPanelManager.LogHandler = TestHelpers.Log;
        AdpUiPanelManager.LogWarningHandler = TestHelpers.LogWarning;
        AdpUiPanelManager.LogErrorHandler = TestHelpers.LogError;

        foreach (var module in TestModules)
        {
            if (module.Exclude) continue;
            TestHelpers.Log($"Test Module: {module.Name}");
            await module.RunTestAsync();
        }

        TestHelpers.Log("Test Finish");
    }
}
