// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public partial class GraphicsDeviceManager
    {
        partial void PlatformInitialize(PresentationParameters presentationParameters)
        {

        }

        partial void PlatformPreparePresentationParameters(PresentationParameters presentationParameters)
        {
            if (_game?.Window?.ClientBounds != null)
            {
                presentationParameters.BackBufferWidth = _game.Window.ClientBounds.Width;
                presentationParameters.BackBufferHeight = _game.Window.ClientBounds.Height;
            }
        }

    }
}