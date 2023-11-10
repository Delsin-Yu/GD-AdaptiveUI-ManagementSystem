using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Test;

public partial class TestManagerImpl : Control
{
    [Export] private TestModule[] TestModules { get; set; }
    [Export] private Label Path { get; set; }

    private Viewport m_Viewport;
    private Control m_CurrentFocusOwner;

    public override void _Ready() => 
        RunTestImplAsync()
           .Forget(exception => throw exception);

    private async GDTask RunTestImplAsync()
    {
        m_Viewport = GetViewport();
        
        AdpUIPanelManager.LogHandler = TestHelpers.Log;
        AdpUIPanelManager.LogWarningHandler = TestHelpers.LogWarning;
        AdpUIPanelManager.LogErrorHandler = TestHelpers.LogError;

        foreach (var module in TestModules)
        {
            if (module.Exclude) continue;
            TestHelpers.Log($"Test Module: {module.Name}");
            await module.RunTestAsync();
        }

        TestHelpers.Log("Test Finish");
        GetTree().Quit();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        var focusOwner = m_Viewport.GuiGetFocusOwner();

        if (m_CurrentFocusOwner == focusOwner)
        {
            return;
        }

        m_CurrentFocusOwner = focusOwner;
        
        Path.Text = $"Selected: {m_CurrentFocusOwner?.GetPath() ?? "Null"}";
    }
}
