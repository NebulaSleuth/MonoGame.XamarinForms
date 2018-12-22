using Example1.Controls;
using Microsoft.Xna.Framework;
using NeonShooter;
using Platformer2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Example1
{
    public partial class MainPage : ContentPage
    {
        bool _ready = false;
        Game _currentGame;

        public MainPage()
        {
            InitializeComponent();


        }

        public void MonoGameInitialized()
        {
            _ready = true;

        }

        public void Close()
        {
            DisposeGame();
        }

        private void DisposeGame()
        {
            _currentGame?.Dispose();
            GameContainer.Children.Clear();
        }

        private void AddGame(Xamarin.Forms.Layout<View> container, Type gameType, float aspect)
        {
            _currentGame = App.GameManager.PlatformInit(gameType, aspect);

            var gv = new GameView();
            gv.AspectRatio = aspect;
            gv.AutoStart = true;
            gv.HorizontalOptions = LayoutOptions.FillAndExpand;
            gv.VerticalOptions = LayoutOptions.FillAndExpand;
            gv.Game = _currentGame;
            container.Children.Add(gv);
            gv.ForceLayout();
        }
        private void Game1_Clicked(object sender, EventArgs e)
        {
            if (!_ready) return;

            DisposeGame();
            AddGame(GameContainer, typeof(PlatformerGame), 1.778f);
        }

        private void Game2_Clicked(object sender, EventArgs e)
        {
            if (!_ready) return;

            DisposeGame();
            AddGame(GameContainer, typeof(NeonShooterGame), 0.0f);

        }

    }
}
