using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class RadioButton : BaseUiObject
    {
        private SelectionItemPattern _pattern;

        public RadioButton(AutomationElement automationElement) : base(automationElement)
        {
            if (AutomationElement == null) return;
            _pattern = (SelectionItemPattern)automationElement.GetCurrentPattern(SelectionItemPattern.Pattern);
        }

        public bool IsSelected
        {
            get
            {
                if (AutomationElement == null) return false;
                return _pattern.Current.IsSelected;
            }
        }

        public override void Click()
        {
            if (AutomationElement == null) return;

            if (!IsSelected && IsEnabled)
                _pattern.Select();
        }

        public static implicit operator RadioButton(AutomationElement automationElement)
        {
            return new RadioButton(automationElement);
        }
    }
}
