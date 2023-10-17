namespace DEYU.GDUtilities.AdpUiManagementSystem;

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
public enum PanelOpenMode
{
    /// <summary>
    ///     禁用当前层的所有UI并且在新的面板层开启目标UI
    /// </summary>
    DisableCurrentUi,

    /// <summary>
    ///     在不禁用当前层的UI的情况下，于同层开启新的UI
    /// </summary>
    PreserveCurrentUi
}

/// <summary>
///     面板可见状态（仅在<see cref="PanelOpenMode" />为<see cref="PanelOpenMode.DisableCurrentUi" />时可用）
/// </summary>
public enum PanelVisualMode
{
    /// <summary>
    ///     不隐藏被禁用层的所有UI
    /// </summary>
    PreserveVisual,

    /// <summary>
    ///     隐藏被禁用层的所有UI
    /// </summary>
    HideVisual
}

public enum InputActionPhase
{
    Pressed,
    Released,
    Any
}