using System;
using System.Collections.Generic;
using System.Text;
using DEYU.GDUtilities.AdpUiManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUiManagementSystem;

internal class EmptyPanelStackException : Exception
{
    public EmptyPanelStackException(StringBuilder lastPanelStack) : base(
        $"""
         当前面板栈为空！是否多次关闭一个面板？最后一次的非空面板栈为：
         {lastPanelStack}
         """
    ) { }
}

internal class IncorrectPanelClosingOrderException : Exception
{
    public StringBuilder LastPanelStack { get; }
    public Stack<Stack<UiPanelBaseImpl>> CurrentPanelStack { get; }

    public UiPanelBaseImpl ClosingPanel { get; }
    public UiPanelBaseImpl TopPanel { get; }


    public IncorrectPanelClosingOrderException
        (
            StringBuilder lastPanelStack,
            Stack<Stack<UiPanelBaseImpl>> currentPanelStack,
            UiPanelBaseImpl closingPanel,
            UiPanelBaseImpl topPanel
        ) : base(
        $"""
         Unable to close the current panel ({closingPanel.Name})
         Because there were other panels opened afterwards ({topPanel.Name})
         Therefore, until those panels are closed, this panel cannot be closed

         无法关闭当前面板({closingPanel.Name})
         因为在此面板之后还开启过其他面板({topPanel.Name})
         因此，在关闭那些面板之前，这个面板是无法关闭的
         """
    )
    {
        LastPanelStack = lastPanelStack;
        CurrentPanelStack = currentPanelStack;
        ClosingPanel = closingPanel;
        TopPanel = topPanel;
    }
}
