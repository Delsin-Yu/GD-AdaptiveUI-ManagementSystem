using System.Linq;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_3_PanelStack_Impl : TestModule
{
    [Export] private PackedScene Panel { get; set; }
    [Export] private Control PanelRoot { get; set; }
    [Export] private Label Path { get; set; }

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

    public override void _Process(double delta)
    {
        base._Process(delta);
        Path.Text = GetViewport().GuiGetFocusOwner()?.GetPath() ?? "Null";
    }
}
