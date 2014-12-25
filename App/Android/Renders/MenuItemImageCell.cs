using Android.Views;
using SportsScheduler.Android.Renders;
using SportsScheduler.Helpers;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Graphics;

using Color = Xamarin.Forms.Color;
using View = global::Android.Views.View;
using ViewGroup = global::Android.Views.ViewGroup;
using Context = global::Android.Content.Context;
using ListView = global::Android.Widget.ListView;

[assembly: ExportCell(typeof(MenuItemImageCell), typeof(MenuItemImageCellRenderer))]

namespace SportsScheduler.Android.Renders
{
    public class MenuItemImageCellRenderer : ImageCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = (LinearLayout)base.GetCellCore(item, convertView, parent, context);
            cell.SetPadding(20, 30, 0, 30);
            cell.DividerPadding = 50;

            var div = new ShapeDrawable();
            div.SetIntrinsicHeight(1);
            div.Paint.Set(new Paint { Color = Color.FromHex("00FFFFFF").ToAndroid() });

            if (parent is ListView)
            {
                ((ListView)parent).Divider = div;
                ((ListView)parent).DividerHeight = 1;
            }


            var image = (ImageView)cell.GetChildAt(0);
            image.SetScaleType(ImageView.ScaleType.FitCenter);

            image.LayoutParameters.Width = 60;
            image.LayoutParameters.Height = 60;


            var linear = (LinearLayout)cell.GetChildAt(1);
            linear.SetGravity(GravityFlags.CenterVertical);

            var label = (TextView)linear.GetChildAt(0);
            label.TextSize = Font.SystemFontOfSize(NamedSize.Large).ToScaledPixel() * 1.25f;
            label.Gravity = (GravityFlags.CenterVertical);
            label.SetTextColor(Color.FromHex("000000").ToAndroid());
            var secondaryLabel = (TextView)linear.GetChildAt(1);
            secondaryLabel.Visibility = ViewStates.Gone;

            return cell;
        }
    }
}