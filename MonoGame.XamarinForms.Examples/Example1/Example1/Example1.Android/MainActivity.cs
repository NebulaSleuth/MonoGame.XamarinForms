using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Xna.Framework;

namespace Example1.Droid
{
    [Activity(Label = "Example1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidFormsGameActivity, View.IOnSystemUiVisibilityChangeListener
    {
        public void OnSystemUiVisibilityChange([GeneratedEnum] StatusBarVisibility visibility)
        {
            HideSystemUi();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            App.GameManager = new GameManager();

            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            Window.DecorView.SetOnSystemUiVisibilityChangeListener(this);
            HideSystemUi();


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var app = new App();
            LoadApplication(app);

            Game.InitStaticData();
            // Let everyone know that we can create games now
            app.MonoGameInitialized();
        }


        private void HideSystemUi()
        {

            if (Window != null)
            {
                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                if (Window.DecorView != null)
                {
                    //Window.DecorView.SystemUiVisibility = (StatusBarVisibility) 0x1707;
                    if (Build.VERSION.SdkInt > BuildVersionCodes.IceCreamSandwichMr1)
                        Window.DecorView.SystemUiVisibility = (StatusBarVisibility)0x1F07;
                }
            }
            ActionBar?.Show();
        }

        protected override void OnStop()
        {
            base.OnStop();

            Game.DisposeStaticData();
        }
    }
}
