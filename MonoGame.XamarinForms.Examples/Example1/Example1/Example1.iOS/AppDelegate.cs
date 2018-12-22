using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Xna.Framework;
using UIKit;

namespace Example1.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.GameManager = new GameManager();

            global::Xamarin.Forms.Forms.Init();
            var theApp = new App();
            LoadApplication(theApp);
            UIApplication.SharedApplication.StatusBarHidden = true;
            //UIApplication.SharedApplication.StatusBarHidden = true;
            var rtn = base.FinishedLaunching(app, options);

            // We need to set the MainWindow for the UI elements before creating any game objects.  
            Game.MainWindow = UIApplication.SharedApplication.KeyWindow;

            // Now that window is set, Let everyone know that we can create games
            theApp.MonoGameInitialized();

            return rtn;
        }
    }
}
