﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace LH.Forcas.Droid
{
    [Activity(Label = "Forcas", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            Plugin.Iconize.Iconize
                  //.With(new Plugin.Iconize.Fonts.FontAwesomeModule());
                  .With(new Plugin.Iconize.Fonts.MaterialModule());

            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            this.LoadApplication(new App());
        }
    }
}