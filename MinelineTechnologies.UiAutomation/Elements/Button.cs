using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Button : BaseUiObject
    {
        public Button(AutomationElement automationElement) : base(automationElement) { }
        
        public static implicit operator Button(AutomationElement automationElement)
        {
            return new Button(automationElement);
        }
    }
}
