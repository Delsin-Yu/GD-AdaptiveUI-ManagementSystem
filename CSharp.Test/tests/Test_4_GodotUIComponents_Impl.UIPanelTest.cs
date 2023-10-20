using System.Runtime.CompilerServices;
using DEYU.GDUtilities.AdpUIManagementSystem.Panels;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class Test_4_GodotUIComponents_Impl
{
    public partial class UIPanelTest : UIPanel
    {
        [Export] private Button ButtonTest { get; set; }
        [Export] private CheckBox CheckBoxTest { get; set; }
        [Export] private CheckButton CheckButtonTest { get; set; }
        [Export] private MenuButton MenuButtonTest { get; set; }
        [Export] private OptionButton OptionButtonTest { get; set; }

        [Export] private HSlider HSliderTest { get; set; }
        [Export] private VSlider VSliderTest { get; set; }
        [Export] private ProgressBar ProgressBarTest { get; set; }
        [Export] private TextureProgressBar TextureProgressBarTest { get; set; }
        [Export] private HScrollBar HScrollBarTest { get; set; }
        [Export] private VScrollBar VScrollBarTest { get; set; }

        protected override void OnPanelInitialize()
        {
            EnableCloseWithCancelKey();

            InitButton(ButtonTest);
            InitButton(CheckBoxTest);
            InitButton(CheckButtonTest);
            InitButton(MenuButtonTest);
            InitButton(OptionButtonTest);

            var menuButtonPopup = MenuButtonTest.GetPopup();
            
            menuButtonPopup.IdFocused += itemIndex => TestHelpers.Log($"{nameof(MenuButtonTest)}::IdFocused({itemIndex}/{menuButtonPopup.GetItemText((int)itemIndex)})");
            menuButtonPopup.IdPressed += itemIndex => TestHelpers.Log($"{nameof(MenuButtonTest)}::IdPressed({itemIndex}/{menuButtonPopup.GetItemText((int)itemIndex)})");
            menuButtonPopup.IndexPressed += itemIndex => TestHelpers.Log($"{nameof(MenuButtonTest)}::IndexPressed({itemIndex}/{menuButtonPopup.GetItemText((int)itemIndex)})");
            menuButtonPopup.MenuChanged += () => TestHelpers.Log($"{nameof(MenuButtonTest)}::MenuChanged");
            
            for (var i = 0; i < 20; i++)
            {
                OptionButtonTest.AddItem($"OptionButtonTest.Item#{i:N2}");
                menuButtonPopup.AddItem($"MenuButtonTest.Item#{i:N2}");
            }

            OptionButtonTest.ItemFocused += itemIndex => TestHelpers.Log($"{nameof(OptionButtonTest)}::ItemFocused({itemIndex}/{OptionButtonTest.GetItemText((int)itemIndex)})");
            OptionButtonTest.ItemSelected += itemIndex => TestHelpers.Log($"{nameof(OptionButtonTest)}::ItemSelected({itemIndex}/{OptionButtonTest.GetItemText((int)itemIndex)})");
            
            InitSlider(HSliderTest, ProgressBarTest);
            InitSlider(VSliderTest, TextureProgressBarTest);
            
            InitScrollBar(HScrollBarTest);
            InitScrollBar(VScrollBarTest);
        }

        private static void InitSlider(Slider slider, Range range, [CallerArgumentExpression(nameof(slider))] string name = null)
        {
            slider.ValueChanged +=
                progress =>
                {
                    TestHelpers.Log($"{name}::ValueChanged({progress:N2})");
                    range.Value = progress;
                };
            InitControl(slider, name);
        }


        private static void InitScrollBar(ScrollBar scrollBar, [CallerArgumentExpression(nameof(scrollBar))] string name = null)
        {
            scrollBar.ValueChanged += progress => TestHelpers.Log($"{name}::ValueChanged({progress:N2})");
            InitControl(scrollBar, name);
        }

        private static void InitButton(BaseButton baseButton, [CallerArgumentExpression(nameof(baseButton))] string name = null)
        {
            baseButton.Pressed += () => TestHelpers.Log($"{name}::Pressed");
            baseButton.Toggled += isToggledOn => TestHelpers.Log($"{name}::Toggled({isToggledOn})");
            InitControl(baseButton, name);
        }

        private static void InitControl(Control control, [CallerArgumentExpression(nameof(control))] string name = null) => 
            control.FocusEntered += () => TestHelpers.Log($"{name}::FocusEntered");

        protected override void OnPanelOpen() => ButtonTest.GrabFocus();
    }
}
