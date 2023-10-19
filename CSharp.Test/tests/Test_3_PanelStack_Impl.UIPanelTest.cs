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
        [Export] private Button NewPanelSameLayerHideOld { get; set; }
        [Export] private Button NewPanelNextLayerHideOld { get; set; }

        protected override void OnPanelInitialize()
        {
            base.OnPanelInitialize();
            EnableCloseWithCancelKey();

            NewPanelSameLayer.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("PanelOpenMode.PreserveCurrentUI, PanelVisualMode.PreserveVisual", OpenParam.Item2), PanelOpenMode.PreserveCurrentUI, PanelVisualMode.PreserveVisual);
            NewPanelNextLayer.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("PanelOpenMode.DisableCurrentUI, PanelVisualMode.PreserveVisual", OpenParam.Item2), PanelOpenMode.DisableCurrentUI, PanelVisualMode.PreserveVisual);
            NewPanelSameLayerHideOld.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("PanelOpenMode.PreserveCurrentUI, PanelVisualMode.HideVisual", OpenParam.Item2), PanelOpenMode.PreserveCurrentUI, PanelVisualMode.HideVisual);
            NewPanelNextLayerHideOld.Pressed +=
                () =>
                    OpenParam.Item2
                       .InstantiateTempPanel<UIPanelTest>()
                       .OpenPanelStack(("PanelOpenMode.DisableCurrentUI, PanelVisualMode.HideVisual", OpenParam.Item2), PanelOpenMode.DisableCurrentUI, PanelVisualMode.HideVisual);
        }

        protected override void OnPanelOpen((string, PackedScene) param)
        {
            LabelText.Text = param.Item1;
            NewPanelSameLayer.GrabFocus();
        }
    }
}
