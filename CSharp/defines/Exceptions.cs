using System;
using System.Collections.Generic;
using System.Text;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

/// <summary>
/// Represents error that occur when incorrectly closing panel
/// </summary>
internal class EmptyPanelStackException : Exception
{
    public EmptyPanelStackException(StringBuilder lastPanelStack) : base(
        $"""
         Current Panel Stack is Empty! Are you closing a panel multiple times? The last non-empty panel stack is the following:
         {lastPanelStack}
         """
    ) { }
}


/// <summary>
/// Represents error that occur when closing panel in incorrect order
/// </summary>
internal class IncorrectPanelClosingOrderException : Exception
{
    public StringBuilder LastPanelStack { get; }
    public Stack<Stack<UIPanelBaseImpl>> CurrentPanelStack { get; }

    public UIPanelBaseImpl ClosingPanel { get; }
    public UIPanelBaseImpl TopPanel { get; }


    public IncorrectPanelClosingOrderException
        (
            StringBuilder lastPanelStack,
            Stack<Stack<UIPanelBaseImpl>> currentPanelStack,
            UIPanelBaseImpl closingPanel,
            UIPanelBaseImpl topPanel
        ) : base(
        $"""
         Unable to close the current panel ({closingPanel.Name})
         Because there were other panels opened afterwards ({topPanel.Name})
         Therefore, until those panels are closed, this panel cannot be closed
         """
    )
    {
        LastPanelStack = lastPanelStack;
        CurrentPanelStack = currentPanelStack;
        ClosingPanel = closingPanel;
        TopPanel = topPanel;
    }
}
