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

[assembly: ExportRenderer(typeof(Example1.Controls.GameView), typeof(Example1.UWP.Renderers.GameViewRenderer))]
namespace Example1.UWP.Renderers
{
    public class GameViewRenderer : ViewRenderer<Example1.Controls.GameView, SwapChainPanel>
    {
        Example1.Controls.GameView _gameView;
        SwapChainPanel _nativeCtrl;
        Game _game;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (Element != null)
                {
                    if (e.PropertyName == "Game")
                    {
                        InitNativeControl();
                    }
                    UpdateElement(Element);
                }
                base.OnElementPropertyChanged(sender, e);

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
                if (_nativeCtrl != null)
                {
                }

                _game = _gameView.Game;
                _nativeCtrl = new SwapChainPanel();

                _game.SwapChainPanel = _nativeCtrl;

                var graphicsDeviceManager = _game.Services.GetService(typeof(GraphicsDeviceManager)) as GraphicsDeviceManager;
                if (graphicsDeviceManager == null)
                {
                    graphicsDeviceManager = new GraphicsDeviceManager(_game);
                    _game.Services.AddService(typeof(GraphicsDeviceManager), graphicsDeviceManager);
                }
                graphicsDeviceManager.SwapChainPanel = _nativeCtrl;

                SetNativeControl(_nativeCtrl);

                if (_gameView.AutoStart) _game?.Run();
            }
        }


        private void UpdateElement(Example1.Controls.GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Example1.Controls.GameView> e)
        {

            try
            {
                if (Control == null)
                {
                    //_nativeCtrl = null;
                }
                if (e.OldElement != null)
                {
                }
                if (e.NewElement != null)
                {
                    _gameView = e.NewElement;
                    InitNativeControl();

                    UpdateElement(e.NewElement);
                }
                else
                {
                    if (_nativeCtrl != null)
                    {
                        //_nativeCtrl.Host?.Stop();
                        //_nativeCtrl = null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
            base.OnElementChanged(e);

        }
    }
}
