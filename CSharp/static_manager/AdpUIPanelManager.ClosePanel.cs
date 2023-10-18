using System;
using System.Text;
using DEYU.GDUtilities.AdpUIManagementSystem.Core;

namespace DEYU.GDUtilities.AdpUIManagementSystem;

public static partial class AdpUIPanelManager
{
    internal static void HandlePanelClose(UIPanelBaseImpl uiPanelBaseImpl, PanelOpenMode currentPanelOpenMode, PanelVisualMode lastPanelVisualMode)
        => Impl.HandlePanelCloseImpl(uiPanelBaseImpl, currentPanelOpenMode, lastPanelVisualMode);

    private partial class AdpUIPanelManagerImpl
    {
        private readonly StringBuilder m_LastSucceedPanelStack = new();

        public void HandlePanelCloseImpl(UIPanelBaseImpl closingPanel, PanelOpenMode currentPanelOpenMode, PanelVisualMode lastPanelVisualMode)
        {
            Log($"[ADP UI] Close Panel: {closingPanel.Name}");
            
            ThrowIfPanelStackIsEmpty();

            RebuildPanelStackString(m_LastSucceedPanelStack);
            
            // 如果该面板是以同层逻辑被打开的，则寻找当前层
            if (currentPanelOpenMode == PanelOpenMode.PreserveCurrentUI)
            {
                var topPanel = m_PanelStack.Peek().Peek();
                
                ThrowIfClosingPanelIsNotTopPanel(closingPanel, topPanel);

                m_PanelStack.Peek().Pop();

                if (m_PanelStack.Peek().Count == 0)
                {
                    PopPanelStack();
                    // 如果弹出之后，面板栈深度 = 0，则切换成默认的操作模式
                    if (m_PanelStack.Count == 0)
                    {
                        UpdateInputScheme(AdpUIInputScheme.UIInputScheme);
                        return;
                    }
                }
                
                // 否则，切换为面板栈顶面板所需的操作模式
                var currentLayer = m_PanelStack.Peek();
                UpdateInputScheme(currentLayer.Peek().RequestedInputScheme);
            }
            // 如果该面板是以叠层逻辑被打开的，则寻找并且弹出当前层
            else
            {
                if (m_PanelStack.Peek().Count > 1) throw new InvalidOperationException($"当前面板栈长度大于一！是否在关闭子面板前就在关闭父面板？最后一次的非空面板栈为：{m_LastSucceedPanelStack}");
                var topPanel = m_PanelStack.Peek().Peek();
                
                ThrowIfClosingPanelIsNotTopPanel(closingPanel, topPanel);
                
                PopPanelStack();

                // 如果存在面板栈，则将顶层面版栈中的面板开启，并且将输入模式切换为栈顶面板需求的
                if (m_PanelStack.Count > 0)
                {
                    var currentLayer = m_PanelStack.Peek();
                    UpdateInputScheme(currentLayer.Peek().RequestedInputScheme);

                    var reselectionBuffer = false;

                    foreach (var panel in currentLayer)
                    {
                        panel.SetPanelActiveState(true, lastPanelVisualMode);
                        panel.HandlePanelReselection(ref reselectionBuffer);
                    }
                }
                else
                {
                    UpdateInputScheme(AdpUIInputScheme.UIInputScheme);
                }
            }
        }

        private void ThrowIfClosingPanelIsNotTopPanel(UIPanelBaseImpl closingPanel, UIPanelBaseImpl topPanel)
        {
            if (!ReferenceEquals(closingPanel, topPanel))
            {
                throw new IncorrectPanelClosingOrderException(
                    m_LastSucceedPanelStack,
                    m_PanelStack,
                    closingPanel,
                    topPanel
                );
            }
        }

        private void ThrowIfPanelStackIsEmpty()
        {
            if (m_PanelStack.Count == 0)
            {
                throw new EmptyPanelStackException(m_LastSucceedPanelStack);
            }
        }

        private void RebuildPanelStackString(StringBuilder builder)
        {
            builder.Clear();

            if (m_PanelStack.Count == 0)
            {
                builder.Append("[ROOT]");
                return;
            }
            var currentIdent = 1;
        
            foreach (var layer in m_PanelStack)
            {
                builder.AppendJoin(" ← ", layer);
                builder.AppendLine();
                builder.Append(' ', currentIdent);
                builder.AppendLine(" ├┄┄┄┄┄┄┄┄┄┄┄┄┄┄");
                if(currentIdent == m_PanelStack.Count - 1) break;
                builder.Append(' ', currentIdent);
                builder.Append(" └┬← ");
                currentIdent++;
            }

            builder.Append(' ', currentIdent - 1);
            builder.AppendLine("[ROOT]");
        }
    }
}
