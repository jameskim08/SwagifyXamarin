using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Swagify.Droid
{
    [Activity(Label = "Swagify", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public partial/*GORILLA*/class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //LoadApplication(UXDivers.Gorilla.Droid.Player.CreateApplication(
            //    this,
            //    new UXDivers.Gorilla.Config("Gorilla on jameskim-pc")
            //      .RegisterAssemblyFromType<Swagify.EmbeddedImage>()
            //    ));
            LoadApplication(new App());
        }
    }
}