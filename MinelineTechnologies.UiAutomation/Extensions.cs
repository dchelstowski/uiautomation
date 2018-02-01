using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation
{
    public static class Extensions
    {
        public static bool IsVisible(this IUiElement automationUiElement)
        {
            if (automationUiElement.Exists())
                return !automationUiElement.AutomationElement.Current.IsOffscreen;

            return false;
        }

        public static bool Exists(this IUiElement automationUiElement)
        {
            return automationUiElement.AutomationElement != null;
        }

        public static AutomationElement FindElement(this IUiElement automationUiElement, By by, string name)
        {
            const TreeScope treeScope = TreeScope.Descendants;
            AutomationElement automationElement = null;

            var sw = new Stopwatch();
            sw.Start();

            while (automationElement == null && sw.Elapsed < TimeSpan.FromSeconds(10))
            {
                switch (by)
                {
                    case By.AutomationId:
                        automationElement = automationUiElement.AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.AutomationIdProperty, name));
                        break;

                    case By.ClassName:
                        automationElement = automationUiElement.AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.ClassNameProperty, name));
                        break;

                    case By.Name:
                        automationElement = automationUiElement.AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.NameProperty, name));
                        break;

                    case By.ControlType:
                        throw new ArgumentException(
                            "To use ControlType parameter use extension method with additional parameter ControlType");

                    default:
                        throw new ArgumentOutOfRangeException(nameof(@by), @by, null);
                }
            }

            sw.Stop();
            return automationElement;
        }

        public static AutomationElement FindElement(this IUiElement automationUiElement, By by, ControlType ctlType,
            string name = null)
        {
            AutomationElement automationElement = null;
            var sw = new Stopwatch();
            sw.Start();

            {
                if (name != null)
                {
                    var elemsList = automationUiElement.AutomationElement.FindAll(TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ctlType));

                    foreach (AutomationElement e in elemsList)
                    {
                        if (e.Current.Name == name)
                            automationElement = e;
                    }
                }
                else
                {
                    automationElement = automationUiElement.AutomationElement.FindFirst(TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ctlType));
                }
            }

            return automationElement;
        }

        public static AutomationElement FindElement(this IUiElement automationUiElement, By by, ControlType ctlType,
            int index)
        {
            AutomationElement automationElement = null;
            var sw = new Stopwatch();
            sw.Start();



            var elemsList = automationUiElement.AutomationElement.FindAll(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.ControlTypeProperty, ctlType));

            if (elemsList.Count > index)
                return elemsList[index];

            else
            {
                automationElement = automationUiElement.AutomationElement.FindFirst(TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ctlType));
            }

            sw.Stop();
            return automationElement;
        }
    }
}
