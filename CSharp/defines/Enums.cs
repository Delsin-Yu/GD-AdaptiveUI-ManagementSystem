namespace DEYU.GDUtilities.AdpUIManagementSystem;

public enum FadeType
{
    /// <summary>
    ///     出现
    /// </summary>
    FadeIn,

    /// <summary>
    ///     消失
    /// </summary>
    FadeOut
}

/// <summary>
///     面板打开模式
/// </summary>
public enum PanelLayer
{
    /// <summary>
    ///     禁用当前层的所有UI并且在新的面板层开启目标UI
    /// </summary>
    NewLayer,

    /// <summary>
    ///     在不禁用当前层的UI的情况下，于同层开启新的UI
    /// </summary>
    SameLayer
}

/// <summary>
///     面板可见状态（仅在<see cref="PanelLayer" />为<see cref="PanelLayer.NewLayer" />时可用）
/// </summary>
public enum LayerVisual
{
    /// <summary>
    ///     不隐藏被禁用层的所有UI
    /// </summary>
    Preserve,

    /// <summary>
    ///     隐藏被禁用层的所有UI
    /// </summary>
    Hide
}

public enum InputActionPhase
{
    Pressed,
    Released,
    Any
}

internal enum SelectionCacheResult
{
    NoSelections,
    SelectionIsNotChild,
    Successful
}