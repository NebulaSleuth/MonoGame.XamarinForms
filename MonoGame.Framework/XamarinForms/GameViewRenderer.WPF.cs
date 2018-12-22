using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using System.ComponentModel;
using Xamarin.Forms.Platform.WPF;
using System.Windows.Controls;
using Example1.WPF;
using Example1.WPF.Controls;
using Microsoft.Xna.Framework.WPFForms;

[assembly: ExportRenderer(typeof(Microsoft.Xna.Framework.XamarinForms.GameView), typeof(Microsoft.Xna.Framework.XamarinForms.GameViewRenderer))]
namespace Microsoft.Xna.Framework.XamarinForms.Renderers
{
    public class GameViewRenderer : ViewRenderer<Microsoft.Xna.Framework.XamarinForms.GameView, Control>
    {
        Microsoft.Xna.Framework.XamarinForms.GameView _gameView;
        WPFMonoGameContainer _nativeCtrl;
        Game _game;
        Image _host;

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
                    _game = _gameView.Game;
                    _nativeCtrl = new WPFMonoGameContainer();
                    _nativeCtrl.Game = _game;
                    //_host = _nativeCtrl.Host;
                    //_gameView.Game.HostControl = _host;
                    //_host.SetGame(_gameView.Game);
                    if (_gameView.AutoStart) _game.Run();

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
                    _nativeCtrl = null;
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
                else
                {
                    if (_nativeCtrl != null)
                    {
                        //_nativeCtrl.Host?.Stop();
                        _nativeCtrl = null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
            }
        }
    }
}
