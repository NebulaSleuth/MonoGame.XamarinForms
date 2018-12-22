// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

#if WINDOWS_UAP
using Windows.UI.ViewManagement;
#endif

namespace Microsoft.Xna.Framework
{
    partial class GamePlatform
    {
        internal static GamePlatform PlatformCreate(Game game)
        {
#if DESKTOPGL || ANGLE
            return new SdlGamePlatform(game);
#elif WINDOWS && DIRECTX
#if FORMS
            return new MonoGame.Framework.WPFFormsGamePlatform(game);
#else
            return new MonoGame.Framework.WinFormsGamePlatform(game);
#endif
#elif WINDOWS_UAP
            return new UAPGamePlatform(game);
#endif
        }
    }
}
