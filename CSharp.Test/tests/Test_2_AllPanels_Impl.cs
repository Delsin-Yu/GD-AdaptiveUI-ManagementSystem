using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_2_AllPanels_Impl : TestModule
{
    [Export] private PackedScene TestUIPanelExtern { get; set; }
    [Export] private PackedScene TestUIPanelParamClose { get; set; }
    [Export] private PackedScene TestUIPanelParamExternOpen { get; set; }
    [Export] private PackedScene TestUIPanelParamOpen { get; set; }
    [Export] private PackedScene TestUIPanelParam { get; set; }
    [Export] private PackedScene TestUIPanel { get; set; }  
    
    
    [Export] private PackedScene TestUIPanelExternAlt { get; set; }
    [Export] private PackedScene TestUIPanelParamExternOpenAlt { get; set; }

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
        var testUIPanelExternTestImpl = TestHelpers.Run(() => TestUIPanelExternAlt.InstantiateTempPanel<UIPanelExternTestAltImpl>());
        TestHelpers.Run(() => testUIPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUIPanelExternTestImpl.CloseExternPanel());

        var testUIPanelParamExternOpenTestImpl = TestHelpers.Run(() => TestUIPanelParamExternOpenAlt.InstantiateTempPanel<UIPanelParamExternOpenTestAltImpl>());
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.OpenPanelStack("TestUIPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.CloseExternPanel());
    }
    
    private void TestExternPanelBuffered()
    {
        var testUIPanelExternTestImpl = TestHelpers.Run(() => TestUIPanelExternAlt.PrepareBufferedPanel<UIPanelExternTestAltImpl>());
        TestHelpers.Run(() => testUIPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUIPanelExternTestImpl.CloseExternPanel());
        TestHelpers.Run(() => testUIPanelExternTestImpl.OpenPanelStack());
        TestHelpers.Run(() => testUIPanelExternTestImpl.CloseExternPanel());
        
        var testUIPanelParamExternOpenTestImpl = TestHelpers.Run(() => TestUIPanelParamExternOpenAlt.PrepareBufferedPanel<UIPanelParamExternOpenTestAltImpl>());
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.OpenPanelStack("TestUIPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.CloseExternPanel());
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.OpenPanelStack("TestUIPanelParamExternOpenAlt::OpenPanelStack"));
        TestHelpers.Run(() => testUIPanelParamExternOpenTestImpl.CloseExternPanel());
    }

    private async GDTask TestAllPanelBuffered()
    {
        await TestHelpers.RunAsync(() => TestUIPanel.PrepareBufferedPanel<UIPanelTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanel.PrepareBufferedPanel<UIPanelTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanel));
        await TestHelpers.RunAsync(() => TestUIPanelExtern.PrepareBufferedPanel<UIPanelExternTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanelExtern.PrepareBufferedPanel<UIPanelExternTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanelExtern));
        await TestHelpers.RunAsync(() => TestUIPanelParam.PrepareBufferedPanel<UIPanelParamTestImpl>().OpenPanelStackAsync("TestUIPanelParam::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUIPanelParam.PrepareBufferedPanel<UIPanelParamTestImpl>().OpenPanelStackAsync("TestUIPanelParam::OpenPanelStack"));
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanelParam));
        await TestHelpers.RunAsync(() => TestUIPanelParamOpen.PrepareBufferedPanel<UIPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUIPanelParamOpen.PrepareBufferedPanel<UIPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamOpen::OpenPanelStack"));
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanelParamOpen));
        await TestHelpers.RunAsync(() => TestUIPanelParamClose.PrepareBufferedPanel<UIPanelParamCloseTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanelParamClose.PrepareBufferedPanel<UIPanelParamCloseTestImpl>().OpenPanelStackAsync());
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanelParamClose));
        await TestHelpers.RunAsync(() => TestUIPanelParamExternOpen.PrepareBufferedPanel<UIPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamExternOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUIPanelParamExternOpen.PrepareBufferedPanel<UIPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamExternOpen::OpenPanelStack"));
        TestHelpers.Run(() => AdpUIPanelManager.TryDeleteBufferedPanel(TestUIPanelParamExternOpen));
    }

    private async GDTask TestAllPanelTemp()
    {
        await TestHelpers.RunAsync(() => TestUIPanel.InstantiateTempPanel<UIPanelTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanelExtern.InstantiateTempPanel<UIPanelExternTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanelParam.InstantiateTempPanel<UIPanelParamTestImpl>().OpenPanelStackAsync("TestUIPanelParam::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUIPanelParamOpen.InstantiateTempPanel<UIPanelParamOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamOpen::OpenPanelStack"));
        await TestHelpers.RunAsync(() => TestUIPanelParamClose.InstantiateTempPanel<UIPanelParamCloseTestImpl>().OpenPanelStackAsync());
        await TestHelpers.RunAsync(() => TestUIPanelParamExternOpen.InstantiateTempPanel<UIPanelParamExternOpenTestImpl>().OpenPanelStackAsync("TestUIPanelParamExternOpen::OpenPanelStack"));
    }
}
