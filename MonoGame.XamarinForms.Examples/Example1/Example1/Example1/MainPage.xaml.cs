using Example1.Controls;
using Microsoft.Xna.Framework;
using NeonShooter;
using Platformer2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Example1
{
    public partial class MainPage : ContentPage
    {
        Game _currentGame;

        public MainPage()
        {
            InitializeComponent();
        }

        public void Close()
        {
            DisposeGame();
        }

        private async Task DisposeGame()
        {
            GameContainer.Children.Clear();
            if (_currentGame != null)
            {
                _currentGame.Exit();
                await _currentGame.WaitForExit();
                _currentGame.Dispose();

            }
        }

        private void AddGame(Xamarin.Forms.Layout<View> container, Game game)
        {
            //_currentGame = App.GameManager.PlatformInit(gameType, aspect);
            _currentGame = game;

            var gv = new GameView();
            gv.AutoStart = true;
            gv.HorizontalOptions = LayoutOptions.FillAndExpand;
            gv.VerticalOptions = LayoutOptions.FillAndExpand;
            gv.Game = _currentGame;
            container.Children.Add(gv);
            gv.ForceLayout();
        }
        private async void Game1_Clicked(object sender, EventArgs e)
        {
            await DisposeGame();
            AddGame(GameContainer, new PlatformerGame());
        }

        private async void Game2_Clicked(object sender, EventArgs e)
        {
            await DisposeGame();
            //AddGame(GameContainer, new NeonShooterGame());

        }

    }
}
