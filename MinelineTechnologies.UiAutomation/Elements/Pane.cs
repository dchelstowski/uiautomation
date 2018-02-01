using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Pane : BaseUiObject
    {
        public Pane(AutomationElement automationElement) : this(null, automationElement) { }

        public Pane(Window mainWindow, AutomationElement automationElement) : base(mainWindow, automationElement) { }

        public static implicit operator Pane(AutomationElement automationElement)
        {
            return new Pane(null, automationElement);
        }
    }
}
