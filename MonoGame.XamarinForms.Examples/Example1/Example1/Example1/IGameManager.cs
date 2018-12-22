using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example1
{
    public interface IGameManager
    {
        event EventHandler OnMonoGameInitialized;

        Game PlatformInit(Type gameType, float aspectRatio);

        void MonoGameInitialized();
    }
}
