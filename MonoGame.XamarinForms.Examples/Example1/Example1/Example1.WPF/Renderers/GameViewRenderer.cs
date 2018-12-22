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
using System.Windows.Media.Imaging;

[assembly: ExportRenderer(typeof(Example1.Controls.GameView), typeof(Example1.WPF.Renderers.GameViewRenderer))]
namespace Example1.WPF.Renderers
{
    public class GameViewRenderer : ViewRenderer<Example1.Controls.GameView, Control>
    {
        Example1.Controls.GameView _gameView;
        WPFMonoGameContainer _nativeCtrl;
        //HostContainer _nativeCtrl;
        Game _game;
        Image _host;

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
                if (_game != null && _game != _gameView.Game)
                {
                    //_game.Dispose();
                }
                //if (_gameView.Game != null)
                {
                    this.
                    _game = _gameView.Game;
                    _game.AspectRatio = _gameView.AspectRatio;
                    _nativeCtrl = new WPFMonoGameContainer();
                    //_nativeCtrl = new HostContainer();
                    //if (_game is Platformer2D.PlatformerGame)
                    //{
                    //    _nativeCtrl.Source = new BitmapImage(new Uri(@"G:\OpenSource\MonoGame.XamarinForms\MonoGame.XamarinForms.Examples\Example1\Example1\Example1.WPF\Images\background8.png", UriKind.Absolute));
                    //}
                    //else
                    //{
                    //    _nativeCtrl.Source = new BitmapImage(new Uri(@"G:\OpenSource\MonoGame.XamarinForms\MonoGame.XamarinForms.Examples\Example1\Example1\Example1.WPF\Images\background4.png", UriKind.Absolute));
                    //}

                    _nativeCtrl.Game = _game;

                    SetNativeControl(_nativeCtrl);
                    //_gameView.ForceLayout();
                    //_host = _nativeCtrl.Host;
                    //_gameView.Game.HostControl = _host;
                    //_host.SetGame(_gameView.Game);
                    if (_gameView.AutoStart) _game?.Run();

                }
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
