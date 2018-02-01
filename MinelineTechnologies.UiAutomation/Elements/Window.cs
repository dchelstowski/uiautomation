using System.Collections.Generic;
using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Window : BaseUiObject
    {
        private WindowPattern _pattern;

        public Window(AutomationElement automationElement) : base(automationElement)
        {
            if (automationElement != null)
                _pattern = (WindowPattern)automationElement.GetCurrentPattern(WindowPattern.Pattern);
        }

        public void Close()
        {
            if (AutomationElement == null) return;
            _pattern.Close();
        }

        public void Maximize()
        {
            if (AutomationElement == null) return;
            _pattern.SetWindowVisualState(WindowVisualState.Maximized);
        }

        public void Minimize()
        {
            if (AutomationElement == null) return;
            _pattern.SetWindowVisualState(WindowVisualState.Minimized);
        }

        public void Normalize()
        {
            if (AutomationElement == null) return;
            _pattern.SetWindowVisualState(WindowVisualState.Normal);
        }

        public bool IsModal
        {
            get
            {
                if (AutomationElement == null) return false;
                return _pattern.Current.IsModal;
            }
        }

        public Window GetModalWindow(string title, int? timeout = null)
        {
            AutomationElement elem = null;

            if (AutomationElement == null) return null;

            elem = timeout == null ? FindElement(By.Name, title) : FindElement(By.Name, title, timeout.Value);

            var windowElement = new Window(elem);
            return windowElement;
        }

        public List<Window> GetAllModalWindows()
        {
            if (AutomationElement == null) return null;

            var allModalWindows = new List<Window>();

            var allElements = FindAllChildElements();
            foreach (AutomationElement e in allElements)
            {
                if (e.Current.ControlType == ControlType.Window)
                {
                    var window = new Window(e);
                    if (window.IsModal)
                        allModalWindows.Add(window);
                }
            }

            return allModalWindows;
        } 
    }
}
