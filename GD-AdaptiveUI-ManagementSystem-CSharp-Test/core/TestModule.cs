using Fractural.Tasks;
using Godot;

namespace DEYU.GDUtilities.AdpUiManagementSystem.Test;

public abstract partial class TestModule : Node
{
    [Export] internal bool Exclude { get; private set; } 
    public abstract GDTask RunTestAsync();
}
