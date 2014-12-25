using SportsScheduler.Helpers;
using SportsScheduler.WinPhone.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Color = Xamarin.Forms.Color;
using DataTemplate = System.Windows.DataTemplate;

[assembly: ExportCell(typeof(MenuItemImageCell), typeof(MenuItemImageCellRenderer))]

namespace SportsScheduler.WinPhone.Renders
{
    public class MenuItemImageCellRenderer : ImageCellRenderer
    {
        public override DataTemplate GetTemplate(Cell cell)
        {
            var menuItemImageCell = (MenuItemImageCell) cell;
            menuItemImageCell.TextColor = Color.Black;

            return base.GetTemplate(cell);
        }
    }
}