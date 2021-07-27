using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using SnailBApp.Droid.CustomRender;
using SnailBApp.Views.CustomLayout;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Entry = Xamarin.Forms.Entry;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererAndroid))]
#pragma warning restore CS0612 // Type or member is obsolete

namespace SnailBApp.Droid.CustomRender
{
    [Obsolete]
    public class CustomEntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(20, 0, 0, 0);

                CustomEntry customEntry = (CustomEntry)e.NewElement;
                if (customEntry.IsPasswordFlag)
                {
                    this.Control.InputType = InputTypes.TextVariationVisiblePassword;
                }

            }
        }
        public CustomEntryRendererAndroid(Context context) : base(context)
        {
        }
    }
}