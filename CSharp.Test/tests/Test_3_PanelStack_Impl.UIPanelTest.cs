using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_3_PanelStack_Impl
{
    public partial class UIPanelTest : UIPanelParamOpen<(string, PackedScene)>
    {
        [Export] private Label LabelText { get; set; }
        [Export] private Button NewPanelSameLayer { get; set; }
        [Export] private Button NewPanelNextLayer { get; set; }
        [Export] private Button NewPanelNextLayerHidePrev { get; set; }

        protected override void OnPanelInitialize()
        {
            base.OnPanelInitialize();
            EnableCloseWithCancelKey();

            NewPanelSameLayer.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("NewPanelSameLayer", OpenParam.Item2), PanelLayer.SameLayer);
            NewPanelNextLayer.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("NewPanelNextLayer", OpenParam.Item2));
            NewPanelNextLayerHidePrev.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("NewPanelNextLayerHidePrev", OpenParam.Item2), lastLayerVisual: LayerVisual.Hidden);
        }

        protected override void OnPanelOpen((string, PackedScene) param)
        {
            LabelText.Text = param.Item1;
            NewPanelSameLayer.GrabFocus();
        }
    }
}
