using Example1.UWP.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Microsoft.Xna.Framework.XamarinForms.GameView), typeof(Microsoft.Xna.Framework.XamarinForms.GameViewRenderer))]
namespace Example1.UWP.Renderers
{
    public class GameViewRenderer : ViewRenderer<Microsoft.Xna.Framework.XamarinForms.GameView, Windows.UI.Xaml.Controls.Control>
    {
        Microsoft.Xna.Framework.XamarinForms.GameView _gameView;
        SwapPanelContainer _nativeCtrl;
        Game _game;

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
                if (_game != null)
                {
                    _game.Dispose();
                }
                if (_gameView.Game != null)
                {
                    _nativeCtrl = new SwapPanelContainer();

                    SetNativeControl(_nativeCtrl);
                }
            }
        }
        private void UpdateElement(Example1.Controls.GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Example1.Controls.GameView> e)
        {
            base.OnElementChanged(e);

            try
            {
                if (Control == null)
                {
                    _nativeCtrl = new SwapPanelContainer();
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
