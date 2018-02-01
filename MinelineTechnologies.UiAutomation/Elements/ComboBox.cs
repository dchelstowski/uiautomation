using System.Windows.Automation;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class ComboBox : BaseUiObject
    {
        private ExpandCollapsePattern _expandCollapsePattern;
        private ValuePattern _valuePattern;

        public ComboBox(AutomationElement automationElement) : base(automationElement)
        {
            if (automationElement == null) return;
            _expandCollapsePattern = (ExpandCollapsePattern)automationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern);
            _valuePattern = (ValuePattern) automationElement.GetCurrentPattern(ValuePattern.Pattern);
        }

        public void Expand()
        {
            if (AutomationElement == null) return;
            _expandCollapsePattern.Expand();
        }

        public void Collapse()
        {
            if (AutomationElement == null) return;
            _expandCollapsePattern.Collapse();
        }

        public void SetText(string text)
        {
            if (AutomationElement == null) return;
            _valuePattern.SetValue(text);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
        }
    }
}
