using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Microsoft.Xna.Framework.XamarinForms.GameView), typeof(Microsoft.Xna.Framework.XamarinForms.GameViewRenderer))]
namespace Microsoft.Xna.Framework.XamarinForms
{
    public class GameViewRenderer : ViewRenderer<Microsoft.Xna.Framework.XamarinForms.GameView, Android.Views.View>
    {
        static Android.Views.View _nativeCtrl;
        Microsoft.Xna.Framework.XamarinForms.GameView _gameView;

        Context _context;

        public GameViewRenderer(Context c) : base(c)
        {
            _context = c;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);
                if (Element != null)
                {
                    if (e.PropertyName == "Game")
                    {
                        InitNativeControl();
                    }
                    UpdateElement(Element);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
        }

        private void InitNativeControl()
        {
            if (_gameView != null)
            {
                if (_gameView.Game != null)
                {
                    var gfxManager = new GraphicsDeviceManager(_gameView.Game);
                    _gameView.Game.Services.AddService(typeof(GraphicsDeviceManager), gfxManager);
                    gfxManager.IsFullScreen = false;

                    _nativeCtrl = (Android.Views.View)_gameView.Game.Services.GetService(typeof(Android.Views.View));
                    SetNativeControl(_nativeCtrl);
                    if (_gameView.AutoStart) _gameView.Game.Run();
                }
            }
        }
        private void UpdateElement(Microsoft.Xna.Framework.XamarinForms.GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.GameView> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (Control == null)
                {
                    _nativeCtrl = new Android.Views.View(Context);
                }
                if (e.OldElement != null)
                {
                }
                if (e.NewElement != null)
                {
                    _gameView = e.NewElement;
                    if (e.NewElement != null && e.NewElement.Game != null)
                    {
                        InitNativeControl();
                    }

                    UpdateElement(e.NewElement);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
        }
    }
}
