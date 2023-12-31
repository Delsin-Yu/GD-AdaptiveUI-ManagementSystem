﻿using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_4_GodotUIComponents_Impl : TestModule
{
    [Export] private PackedScene Panel { get; set; }

    public override GDTask RunTestAsync() => TestHelpers.RunAsync(RunCoreAsync);

    private GDTask RunCoreAsync() => Panel.PrepareBufferedPanel<UIPanel>().OpenPanelStackAsync();
}
