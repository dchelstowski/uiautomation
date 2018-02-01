using System.Diagnostics;
using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Checkbox : BaseUiObject
    {
        private readonly TogglePattern _togglePattern;

        public Checkbox(AutomationElement automationElement) : base(automationElement)
        {
            if (automationElement == null) return;
            _togglePattern = (TogglePattern)AutomationElement.GetCurrentPattern(TogglePattern.Pattern);
        }

        public bool IsChecked
        {
            get
            {
                if (AutomationElement == null) return false;
                return _togglePattern.Current.ToggleState == ToggleState.On;
            }
        }

        public void Check()
        {
            if (AutomationElement == null) return;

            if (!IsChecked)
            {
                Debug.WriteLine($"check {AutomationElement.Current.Name}");
                _togglePattern.Toggle();
            }
            else
            {
                Debug.WriteLine($"{AutomationElement.Current.Name} was checked - no action");

            }
        }

        public void Uncheck()
        {
            if (AutomationElement == null) return;
            
            if (IsChecked)
            {
                Debug.WriteLine($"uncheck {AutomationElement.Current.Name}");
                _togglePattern.Toggle();
            }
            else
            {
                Debug.WriteLine($"{AutomationElement.Current.Name} was checked - no action");
            }
        }

        public static implicit operator Checkbox(AutomationElement automationElement)
        {
            return new Checkbox(automationElement);
        }
    }
}
