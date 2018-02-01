using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation
{
    public interface IClickable
    {
        void Click();

        void RightClick();
    }

    public interface IEditable
    {
        void SetText(string text);
    }

    public interface IDisplayable
    {
        bool IsVisible();

        bool Exists();
    }

    public interface IUiElement
    {
        AutomationElement AutomationElement { get; set; }

        void Click();

        void Click(int timeout);

        void RightClick();
    }

    
}
