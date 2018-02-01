using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class TreeNode : BaseUiObject, IClickable
    {
        private ExpandCollapsePattern _expandCollapsePattern;
        private SelectionItemPattern _selectionPattern;

        public TreeNode(AutomationElement automationElement) : base(automationElement)
        {
            initializePatterns();
        }

        private void initializePatterns()
        {
            if (AutomationElement == null) return;

            _expandCollapsePattern = (ExpandCollapsePattern)AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            _selectionPattern = (SelectionItemPattern)AutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern);
        }

        public override void Click()
        {
            if (AutomationElement == null) return;

            var invPattern = AutomationElement.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;

            if (invPattern == null)
                throw new NoClickablePointException("This element doesn't support Invoke pattern");
            
            invPattern.Select();
        }

        public void Expand()
        {
            if (AutomationElement == null) return;

            try
            {
                _expandCollapsePattern.Expand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("This element doesn't support ExpandCollapse pattern. " + ex.Message);
            }
        }

        public void Collapse()
        {
            if (AutomationElement == null) return;

            _expandCollapsePattern.Collapse();
        }

        public ExpandCollapseState ExpandCollapseState
        {
            get
            {
                if (AutomationElement == null) return ExpandCollapseState.PartiallyExpanded;

                return _expandCollapsePattern.Current.ExpandCollapseState;
            }
        }

        public void Select()
        {
            if (AutomationElement == null) return;

            _selectionPattern.Select();
        }

        public static implicit operator TreeNode(AutomationElement automationElement)
        {
            return new TreeNode(automationElement);
        }
    }
}
