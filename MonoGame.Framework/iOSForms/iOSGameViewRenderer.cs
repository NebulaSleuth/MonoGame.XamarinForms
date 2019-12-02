using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GameView), typeof(Microsoft.Xna.Framework.iOSForms.iOSGameViewRenderer))]
namespace Microsoft.Xna.Framework.iOSForms
{
    public class iOSGameViewRenderer : ViewRenderer<GameView, UIView>
    {
        UIView _nativeCtrl;
        GameView _gameView;

        public iOSGameViewRenderer() : base()
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

        private void OnSizeChanged(object sender, EventArgs e)
        {
            ((IVisualElementController)Element).NativeSizeChanged();
        }


        private void InitNativeControl()
        {

            if (_gameView != null)
            {
                if (_gameView.Game != null)
                {
                    var gfxManager = new GraphicsDeviceManager(_gameView.Game);
                    gfxManager.PreferMultiSampling = true;
                    _gameView.Game.Services.AddService(typeof(GraphicsDeviceManager), gfxManager);
                    gfxManager.IsFullScreen = false;

                    _nativeCtrl = ((UIViewController)_gameView.Game.Services.GetService(typeof(UIViewController)))?.View;
                    _nativeCtrl?.LayoutSubviews();
                    SetNativeControl(_nativeCtrl);
                    if (_gameView.AutoStart) _gameView.Game.Run();
                }
            }
        }

        private void UpdateElement(GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GameView> e)
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
                    e.OldElement.SizeChanged -= OnSizeChanged;
                }
                if (e.NewElement != null)
                {
                    e.NewElement.SizeChanged += OnSizeChanged;
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
