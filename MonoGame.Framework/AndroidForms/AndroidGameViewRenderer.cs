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

[assembly: ExportRenderer(typeof(GameView), typeof(Microsoft.Xna.Framework.AndroidForms.AndroidGameViewRenderer))]
namespace Microsoft.Xna.Framework.AndroidForms
{
    public class AndroidGameViewRenderer : ViewRenderer<GameView, Android.Views.View>
    {
        static Android.Views.View _nativeCtrl;
        GameView _gameView;

        Context _context;

        public AndroidGameViewRenderer(Context c) : base(c)
        {
            _context = c;
        }

        private void UpdateElement(GameView e)
        {
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
                    gfxManager.PreferMultiSampling = false;
                    _gameView.Game.Services.AddService(typeof(GraphicsDeviceManager), gfxManager);
                    gfxManager.IsFullScreen = true;

                    var ctrl = (Android.Views.View)_gameView.Game.Services.GetService(typeof(Android.Views.View));
                    //if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    //{
                    //    ctrl.ClipToOutline = true;
                    //}
                    _nativeCtrl = ctrl;
                    SetNativeControl(_nativeCtrl);
                    _gameView.BackgroundColor = Xamarin.Forms.Color.Black;
                    _gameView.Opacity = 1.0;
                    //((IVisualElementController)Element).NativeSizeChanged();
                    Element.BackgroundColor = Xamarin.Forms.Color.Transparent;
                    if (_gameView.AutoStart) _gameView.Game.Run();
                }
                else
                {
                    RemoveAllViews();
                }
            }
        }


        private void OnSizeChanged(object sender, EventArgs e)
        {
            ((IVisualElementController)Element).NativeSizeChanged();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GameView> e)
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
                    e.OldElement.SizeChanged -= OnSizeChanged;
                }
                if (e.NewElement != null)
                {
                    _gameView = e.NewElement;
                    if (e.NewElement != null && e.NewElement.Game != null)
                    {
                        InitNativeControl();
                    }
                    e.NewElement.SizeChanged += OnSizeChanged;

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
