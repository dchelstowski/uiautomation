using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Label : BaseUiObject
    {
        public Label(AutomationElement automationElement) : base(automationElement) { }

        public static implicit operator Label(AutomationElement automationElement)
        {
            return new Label(automationElement);
        }
    }
}
