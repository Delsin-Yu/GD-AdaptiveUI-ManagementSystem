using System.Linq;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_3_PanelStack_Impl : TestModule
{
    [Export] private PackedScene Panel { get; set; }
    [Export] private Control PanelRoot { get; set; }

    public override async GDTask RunTestAsync()
    {
        TestHelpers.Run(() => AdpUIPanelManager.PushActivePanelTransform(this, PanelRoot));
        await TestHelpers.RunAsync(
            () =>
                Panel
                   .InstantiateTempPanel<UIPanelTest>()
                   .OpenPanelStackAsync(("Base", Panel))
        );
        TestHelpers.Run(() => AdpUIPanelManager.PopActivePanelTransform(this));
    }
}
