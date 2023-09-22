
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using locationsApp.Droid.CustomRenderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace locationsApp.Droid.CustomRenderers
{
    [Obsolete]
    class CustomEntryRenderer : EntryRenderer
    {
        void SetColor(Android.Graphics.Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(color);
            }
            else
            {
                Control.Background.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                SetColor(Android.Graphics.Color.Transparent);

                this.EditText.FocusChange += (sender, ee) =>
                {
                    bool hasFocus = ee.HasFocus;
                    if (hasFocus)
                    {
                        SetColor(Android.Graphics.Color.Transparent);
                    }
                    else
                    {
                        SetColor(Android.Graphics.Color.Transparent);
                    }
                };
            }
        }
    }
}