using Godot;
using System;

namespace DEYU.GDUtilities.AdpUIManagementSystem.Utils;

public partial class PointerButtonMarker : BaseButton
{
    [Export] private bool MousePassThrough { get; set; }
    
    private void EnablePointerInteraction() => 
        MouseFilter = MousePassThrough ? MouseFilterEnum.Pass : MouseFilterEnum.Stop;
}
