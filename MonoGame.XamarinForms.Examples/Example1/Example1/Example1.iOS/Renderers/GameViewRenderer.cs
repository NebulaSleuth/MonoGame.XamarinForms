using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Example1.Controls.GameView), typeof(Example1.iOS.Renderers.GameViewRenderer))]
namespace Example1.iOS.Renderers
{
    public class GameViewRenderer : ViewRenderer<Example1.Controls.GameView, UIView>
    {
        UIView _nativeCtrl;
        Example1.Controls.GameView _gameView;

        public GameViewRenderer() : base()
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
            catch (Exception)
            {
                // Ignore
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

                    _nativeCtrl = ((UIViewController)_gameView.Game.Services.GetService(typeof(UIViewController)))?.View;
                    _nativeCtrl?.LayoutSubviews();
                    SetNativeControl(_nativeCtrl);
                    if (_gameView.AutoStart) _gameView.Game.Run();
                }
            }
        }

        private void UpdateElement(Example1.Controls.GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.GameView> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (Control == null)
                {
                    _nativeCtrl = new UIView();
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
                System.Diagnostics.Debug.WriteLine(@"          ERROR: ", ex.Message);
            }
        }
    }
}
