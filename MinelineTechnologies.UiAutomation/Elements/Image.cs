using System.Drawing;
using System.Windows.Automation;

namespace MinelineTechnologies.UiAutomation.Elements
{
    public class Image : BaseUiObject
    {
        private GridItemPattern _gridItemPattern;

        public Image(AutomationElement automationElement) : base(automationElement)
        {
            _gridItemPattern = (GridItemPattern)AutomationElement.GetCurrentPattern(GridItemPattern.Pattern);
        }
        
        public Rectangle GetRectangle()
        {
            return new Rectangle((int) AutomationElement.GetClickablePoint().X,
                (int) AutomationElement.GetClickablePoint().Y,
                _gridItemPattern.Current.Column, _gridItemPattern.Current.Row);
        }

        public Bitmap GetImage()
        {
            var image = new Bitmap(GetRectangle().Width, GetRectangle().Height);
            var graphics = Graphics.FromImage(image);
            graphics.CopyFromScreen(GetRectangle().X, GetRectangle().Y, 0, 0,
                new Size(GetRectangle().Width, GetRectangle().Height));

            return image;
        }
    }
}
