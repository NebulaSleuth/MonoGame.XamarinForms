using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Example1
{
    public class GameManager : IGameManager
    {
        public event EventHandler OnMonoGameInitialized;
        public GraphicsDevice GraphicsDevice { get; set; }
        public Game PlatformInit(Type gameType, float aspectRatio)
        {
            Game.DefaultAspectRatio = aspectRatio;
            return Activator.CreateInstance(gameType) as Game;
        }

        void IGameManager.MonoGameInitialized()
        {
            OnMonoGameInitialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
