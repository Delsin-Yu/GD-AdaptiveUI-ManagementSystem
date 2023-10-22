using Godot;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

/// <summary>
/// Defines the behaviour when performing visual tween on the specified element(s)
/// </summary>
public enum FadeType
{
    /// <summary>
    /// Element is appearing
    /// </summary>
    FadeIn,

    /// <summary>
    /// Element is disappearing
    /// </summary>
    FadeOut
}

/// <summary>
/// Defines the behaviour when opening a new panel
/// </summary>
public enum PanelLayer
{
    /// <summary>
    /// Opens the panel in new panel layer, which means every panel inside the previous layer will no longer focusable or react to pointer click
    /// </summary>
    NewLayer,

    /// <summary>
    /// Opens the panel in current layer, which means every panel inside this layer will remains focusable and react to pointer click if specified via <see cref="Godot.Control.MouseFilter"/>
    /// </summary>
    SameLayer
}

/// <summary>
/// When opening a panel in <see cref="PanelLayer.NewLayer"/> mode, controls the visual status of panels inside the previous layer
/// </summary>
public enum LayerVisual
{
    /// <summary>
    /// When opening a panel in <see cref="PanelLayer.NewLayer"/> mode, every panel inside the previous layer remains visible
    /// </summary>
    Visible,

    /// <summary>
    /// When opening a panel in <see cref="PanelLayer.NewLayer"/> mode, every panel will fades out and become hidden
    /// </summary>
    Hidden
}

/// <summary>
/// Define the input phase of a specific <see cref="Godot.InputEvent"/>
/// </summary>
public enum InputActionPhase
{
    /// <summary>
    /// Triggers when the <see cref="InputEvent.IsPressed"/> method of the <see cref="InputEvent"/> returns true 
    /// </summary>
    Pressed,
    /// <summary>
    /// Triggers when the <see cref="InputEvent.IsReleased"/> method of the <see cref="InputEvent"/> returns true 
    /// </summary>
    Released,
    /// <summary>
    /// Triggers when the <see cref="InputEvent.IsPressed"/> or the <see cref="InputEvent.IsReleased"/> method of the <see cref="InputEvent"/> returns true
    /// </summary>
    Any
}

/// <summary>
/// Internal enum for indicating the selection cache result when opening panel in <see cref="PanelLayer.NewLayer"/> 
/// </summary>
internal enum SelectionCacheResult
{
    /// <summary>
    /// Nothing is currently selected system wise, the caching enumeration should stop
    /// </summary>
    NoSelections,
    /// <summary>
    /// Currently focusing control is not a child of the specified panel, the caching enumeration should continues
    /// </summary>
    SelectionIsNotChild,
    /// <summary>
    /// Currently focusing control is a child of the specified panel, and is cached successfully, the caching enumeration should stop
    /// </summary>
    Successful
}