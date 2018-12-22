using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Example1
{
    public class GameManager : IGameManager
    {
        public event EventHandler OnMonoGameInitialized;

        public SwapChainPanel Panel { get; set; }
        public CoreWindow Window { get; set; }

        public FormsGame CreateGame(Type gameType)
        {
            var launchParameters = string.Empty;

            return null;//MonoGame.Framework.XamlGame<Game>.Create(gameType, launchParameters, Window, Panel);
        }

        void IGameManager.MonoGameInitialized()
        {
            OnMonoGameInitialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
