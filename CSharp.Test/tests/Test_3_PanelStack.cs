using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_3_PanelStack : TestModule
{
    [Export] private PackedScene Panel;

    public partial class UIPanelTest : UIPanelParamOpen<string>
    {
        [Export] private Label Text { get; set; }

        protected override void OnPanelInitialize()
        {
            base.OnPanelInitialize();
            EnableCloseWithCancelKey();
        }

        protected override void OnPanelOpen(string openParam) => 
            Text.Text = openParam;
    }

    public override GDTask RunTestAsync()
    {
        Panel.InstantiateTempPanel<UIPanelTest>().OpenPanelStack("Base", PanelOpenMode.DisableCurrentUI, PanelVisualMode.PreserveVisual);
        return GDTask.CompletedTask;
    }
}
