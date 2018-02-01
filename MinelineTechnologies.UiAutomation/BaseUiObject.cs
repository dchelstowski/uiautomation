using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using MinelineTechnologies.UiAutomation.Elements;
using TestStack.White.InputDevices;

namespace MinelineTechnologies.UiAutomation
{
    public abstract class BaseUiObject : IUiElement
    {
        private const int GLOBAL_TIMEOUT = 5;

        public AutomationElement AutomationElement { get; set; }

        public Window MainWindow { get; set; }

        protected BaseUiObject(Window mainWindow, AutomationElement automationElement)
        {
            AutomationElement = automationElement;
            MainWindow = mainWindow;
        }

        protected BaseUiObject(AutomationElement automationElement) : this(null, automationElement)
        {
            AutomationElement = automationElement;
        }

        public virtual void Click()
        {
            Debug.WriteLine($"click {this.Text}");
            Click(30);
        }

        public virtual void Click(int timeout)
        {
            var sw = new Stopwatch();
            sw.Start();

            while (!this.IsEnabled && sw.Elapsed < TimeSpan.FromSeconds(timeout)) { Thread.Sleep(1000); }
            
            try
            {
                InvokePattern invokePattern = (InvokePattern) AutomationElement.GetCurrentPattern(InvokePattern.Pattern);

                if (IsEnabled)
                {
                    invokePattern.Invoke();
                }
                else
                    throw new Exception("Element not enabled");
            }
            catch (Exception)
            {
                throw new Exception($"Invoke Pattern is not supported for this UI element! ({AutomationElement.Current.Name}, {AutomationElement.Current.ControlType.ProgrammaticName})");
            }
        }

        public bool IsEnabled
        {
            get
            {
                return AutomationElement != null && AutomationElement.Current.IsEnabled;
            }
        }

        public string ToolTip
        {
            get { return AutomationElement != null ? AutomationElement.Current.HelpText : null; }
        }

        public void ScrollIntoView()
        {
            if (AutomationElement == null) return;

            if (Equals(AutomationElement.Current.ControlType, ControlType.Button))
            {
                Debug.WriteLine("Scroll into view is not supported for Button elements");
                return;
            }
            var scrollPattern = (ScrollItemPattern)AutomationElement.GetCurrentPattern(ScrollItemPattern.Pattern);
            scrollPattern.ScrollIntoView();
        }

        public virtual void HoverAndClick()
        {
            if (AutomationElement == null) return;
            Debug.WriteLine($"hover and click '{AutomationElement.Current.Name.ToUpper()}'");
            Mouse.Instance.Location = AutomationElement.GetClickablePoint();
            Mouse.Instance.Click();
        }

        public virtual void RightClick()
        {
            if (AutomationElement == null) return;

            Debug.WriteLine($"right click '{AutomationElement.Current.Name.ToUpper()}'");
            Mouse.Instance.RightClick(AutomationElement.GetClickablePoint());
        }

        public virtual string Text
        {
            get { return AutomationElement == null ? null : AutomationElement.Current.Name; }
        }

        public AutomationElementCollection FindAllChildElements()
        {
            return AutomationElement == null ? null : AutomationElement.FindAll(TreeScope.Descendants, Condition.TrueCondition);
        }

        public AutomationElement FindElement(By by, string name, int timeout = GLOBAL_TIMEOUT)
        {
            var sw = new Stopwatch();
            if (AutomationElement == null) return null;
            const TreeScope treeScope = TreeScope.Descendants;
            AutomationElement automationElement = null;
            sw.Start();

            while (automationElement == null && sw.Elapsed < TimeSpan.FromSeconds(GLOBAL_TIMEOUT))
            {
                switch (by)
                {
                    case By.AutomationId:
                        automationElement = AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.AutomationIdProperty, name));
                        if (automationElement != null && sw.ElapsedMilliseconds > 1000)
                            Debug.WriteLine($"{sw.ElapsedMilliseconds} ms to find {automationElement.Current.AutomationId}");
                        break;

                    case By.ClassName:
                        automationElement = AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.ClassNameProperty, name));
                        if (automationElement != null && sw.ElapsedMilliseconds > 1000)
                            Debug.WriteLine($"{sw.ElapsedMilliseconds} ms to find {automationElement.Current.ClassName}");
                        break;

                    case By.Name:
                        automationElement = AutomationElement.FindFirst(treeScope,
                            new PropertyCondition(AutomationElement.NameProperty, name));
                        if (automationElement != null && sw.ElapsedMilliseconds > 1000)
                            Debug.WriteLine($"{sw.ElapsedMilliseconds} ms to find {automationElement.Current.Name}");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@by), @by, null);
                }
            }

            sw.Stop();

            if (automationElement == null)
                Debug.WriteLine($"{sw.ElapsedMilliseconds} ms. couldn't find AE");

            return automationElement;
        }

        public AutomationElement FindElement(ControlType ctlType, int index = 0, int timeOut = GLOBAL_TIMEOUT)
        {
            var sw = new Stopwatch();
            
            if (AutomationElement == null) return null;

            int ct = 0;
            AutomationElement automationElement = null;
            AutomationElementCollection autoCollection = null;

            sw.Start();

            do
            {
                autoCollection = AutomationElement.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ctlType));
                var ctrlTypeSpecificElements = new List<AutomationElement>();

                foreach (AutomationElement a in autoCollection)
                    ctrlTypeSpecificElements.Add(a);
                
                if (ctrlTypeSpecificElements.Count >= index + 1)
                    automationElement = ctrlTypeSpecificElements[index];

                Thread.Sleep(500);
                ct++;
            } while (automationElement == null && sw.Elapsed < TimeSpan.FromSeconds(timeOut));

            return automationElement;
        }
    }
    public enum By
    {
        Name,
        AutomationId,
        ControlType,
        ClassName
    }
}
