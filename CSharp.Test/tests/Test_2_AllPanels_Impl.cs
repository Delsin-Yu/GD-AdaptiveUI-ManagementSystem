using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public partial class Test_2_AllPanels_Impl : TestModule
{
    [Export] private PackedScene TestUiPanelExtern { get; set; }
    [Export] private PackedScene TestUiPanelParamClose { get; set; }
    [Export] private PackedScene TestUiPanelParamExternOpen { get; set; }
    [Export] private PackedScene TestUiPanelParamOpen { get; set; }
    [Export] private PackedScene TestUiPanelParam { get; set; }
    [Export] private PackedScene TestUiPanel { get; set; }  
    
    
    [Export] private PackedScene TestUiPanelExternAlt { get; set; }
    [Export] private PackedScene TestUiPanelParamExternOpenAlt { get; set; }

    public override async GDTask RunTestAsync()
    {
        await TestAllPanelTemp();
        await TestAllPanelBuffered();     
        await TestAllPanelTemp();
        await TestAllPanelBuffered();
        await TestAllPanelTemp();
        
        TestExternPanelTemp();
        TestExternPanelBuffered();        
        TestExternPanelTemp();
        TestExternPanelBuffered();
        TestExternPanelTemp();
    }

    private void TestExternPanelTemp()
    {
        var testUiPanelExternTestImpl = TestHelpers.Run(() => TestUiPanelExternAlt.InstantiateTempPanel<UiPanelExternTestAltImpl>());
        TestHelpers.Run(() => testUiPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUiPanelExternTestImpl.CloseExternPanel());

        var testUiPanelParamExternOpenTestImpl = TestHelpers.Run(() => TestUiPanelParamExternOpenAlt.InstantiateTempPanel<UiPanelParamExternOpenTestAltImpl>());
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.OpenPanelStack("TestUiPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.CloseExternPanel());
    }
    
    private void TestExternPanelBuffered()
    {
        var testUiPanelExternTestImpl = TestHelpers.Run(() => TestUiPanelExternAlt.PrepareBufferedPanel<UiPanelExternTestAltImpl>());
        TestHelpers.Run(() => testUiPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUiPanelExternTestImpl.CloseExternPanel());
        TestHelpers.Run(() => testUiPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUiPanelExternTestImpl.CloseExternPanel());
        
        var testUiPanelParamExternOpenTestImpl = TestHelpers.Run(() => TestUiPanelParamExternOpenAlt.PrepareBufferedPanel<UiPanelParamExternOpenTestAltImpl>());
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.OpenPanelStack("TestUiPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.CloseExternPanel());
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.OpenPanelStack("TestUiPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUiPanelParamExternOpenTestImpl.CloseExternPanel());
    }

    private async GDTask TestAllPanelBuffered()
    {
        await TestHelpers.RunAsync(() => TestUiPanel.PrepareBufferedPanel<UiPanelTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanel.PrepareBufferedPanel<UiPanelTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanel));
        await TestHelpers.RunAsync(() => TestUiPanelExtern.PrepareBufferedPanel<UiPanelExternTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanelExtern.PrepareBufferedPanel<UiPanelExternTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanelExtern));
        await TestHelpers.RunAsync(() => TestUiPanelParam.PrepareBufferedPanel<UiPanelParamTestImpl>().OpenPanelStackAsync("TestUiPanelParam::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUiPanelParam.PrepareBufferedPanel<UiPanelParamTestImpl>().OpenPanelStackAsync("TestUiPanelParam::OpenPanelStack"));
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanelParam));
        await TestHelpers.RunAsync(() => TestUiPanelParamOpen.PrepareBufferedPanel<UiPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUiPanelParamOpen.PrepareBufferedPanel<UiPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamOpen::OpenPanelStack"));
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanelParamOpen));
        await TestHelpers.RunAsync(() => TestUiPanelParamClose.PrepareBufferedPanel<UiPanelParamCloseTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanelParamClose.PrepareBufferedPanel<UiPanelParamCloseTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanelParamClose));
        await TestHelpers.RunAsync(() => TestUiPanelParamExternOpen.PrepareBufferedPanel<UiPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamExternOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUiPanelParamExternOpen.PrepareBufferedPanel<UiPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamExternOpen::OpenPanelStack"));
        TestHelpers.Run(() => AdpUiPanelManager.TryDeleteBufferedPanel(TestUiPanelParamExternOpen));
    }

    private async GDTask TestAllPanelTemp()
    {
        await TestHelpers.RunAsync(() => TestUiPanel.InstantiateTempPanel<UiPanelTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanelExtern.InstantiateTempPanel<UiPanelExternTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanelParam.InstantiateTempPanel<UiPanelParamTestImpl>().OpenPanelStackAsync("TestUiPanelParam::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUiPanelParamOpen.InstantiateTempPanel<UiPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUiPanelParamClose.InstantiateTempPanel<UiPanelParamCloseTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUiPanelParamExternOpen.InstantiateTempPanel<UiPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUiPanelParamExternOpen::OpenPanelStack"));
    }
}
