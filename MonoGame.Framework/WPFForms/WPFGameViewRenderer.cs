using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using System.ComponentModel;
using Xamarin.Forms.Platform.WPF;
using System.Windows.Controls;
using Microsoft.Xna.Framework.WPFForms;
using System.Windows.Media.Imaging;

[assembly: ExportRenderer(typeof(GameView), typeof(Microsoft.Xna.Framework.WPFForms.WPFGameViewRenderer))]
namespace Microsoft.Xna.Framework.WPFForms
{
    public class WPFGameViewRenderer : ViewRenderer<GameView, Control>
    {
        GameView _gameView;
        WPFMonoGameContainer _nativeCtrl;
        //HostContainer _nativeCtrl;
        Game _game;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (Element != null)
                {
                    if (e.PropertyName == "Game")
                    {
                        _gameView = Element;
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
                _game = _gameView.Game;
                if (_gameView.Game != null)
                {
                    if (_nativeCtrl == null)
                    {
                        _nativeCtrl = new WPFMonoGameContainer();
                        _nativeCtrl.Game = _game;

                        SetNativeControl(_nativeCtrl);

                        if (_gameView.AutoStart) _game?.Run();
                    }
                    else
                    {
                        _nativeCtrl.Game = _game;
                        if (_gameView.AutoStart) _game?.Run();
                    }
                }
                else
                {
                    if (_nativeCtrl == null)
                    {
                        _nativeCtrl = new WPFMonoGameContainer();
                        _nativeCtrl.Game = null;

                        SetNativeControl(_nativeCtrl);
                    }
                }
            }
        }
        private void UpdateElement(GameView e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GameView> e)
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
