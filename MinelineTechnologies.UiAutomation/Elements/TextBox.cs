using System;
using System.Windows.Automation;
using System.Windows.Forms;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class TextBox : BaseUiObject, IEditable
    {
        public TextBox(AutomationElement automationElement) : base(automationElement) { }
        
        public void SetText(string text)
        {
            if (AutomationElement == null) return;

            try
            {
                object pattern;

                AutomationElement.SetFocus();

                if (!AutomationElement.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
                {
                    SendKeys.SendWait("^{HOME}");
                    SendKeys.SendWait("^+{END}");
                    SendKeys.SendWait("{DEL}");
                    SendKeys.SendWait(text);
                }
                else
                {
                    ((ValuePattern) pattern).SetValue(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static implicit operator TextBox(AutomationElement automationElement)
        {
            return new TextBox(automationElement);
        }
    }
}
